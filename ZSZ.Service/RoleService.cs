using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class RoleService : IRoleService
    {
        public RoleDTO[] GetAll()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.GetAll().AsNoTracking().ToList().Select(m => ToDto(m)).ToArray();
            }
        }

        public RoleDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return ToDto(bs.GetAll().Include(x => x.Permissions).FirstOrDefault(m=>m.Id==id));
                //return ToDto(bs.GetById(id));
            }
        }

        public long GetTotalCount()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.GetTotalCount();
            }
        }

        public long Insert(string name)
        {
            using (MyContext my = new MyContext())
            {
                RoleEntity p = new RoleEntity
                { 
                    Name = name,
                };
                my.Roles.Add(p);
                my.SaveChanges();
                return p.Id;
            }
        }
        public void RolePer(long Rid, long[] DesId)
        {
            using (MyContext my=new MyContext())
            {
                BaseService<RoleEntity> r = new BaseService<RoleEntity>(my);
                var Role = r.GetById(Rid);
                if(Role==null)
                {
                    throw new Exception("不存在" + Rid + "的角色");
                }
                else
                {
                    BaseService<PermissionEntity> p = new BaseService<PermissionEntity>(my);
                    List<PermissionEntity> permission = p.GetAll().ToList().Where(m => DesId.Contains(m.Id)).ToList();
                    foreach (var item in permission)
                    {
                        Role.Permissions.Add(item);
                    }
                }
                my.SaveChanges();
            }
        }
        public bool MarkDeleted(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(my);
                return bs.MarkDeleted(id);
            }
        }
        public RoleDTO ToDto(RoleEntity role)
        {
            PermissionService permissionService = new PermissionService(); 
            RoleDTO roleDTO = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permission = permissionService.GetAll(),
                CreateDateTime = role.CreateDateTime,
            };
            return roleDTO;
        }
        public void Update(long Id, string name, long[] DesId)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<RoleEntity> permissionService = new BaseService<RoleEntity>(my);
                var perModel = permissionService.GetById(Id);//先查出来
                var per = perModel.Permissions.Where(m => perModel.Id == Id).ToArray();
                if (perModel == null)
                {
                    throw new Exception("id不存在");
                }
                else
                {
                    perModel.Name = name;
                    for (int i = 0; i < DesId.Length; i++)
                    {
                        //var per = perModel.Permissions.Where(m => m.Name.Contains(DesId[i]));
                        //if(per==null)
                        //{ }
                        PermissionEntity permission = new PermissionEntity { Id = DesId[i] };
                        perModel.Permissions.Add(permission);
                    }
                    my.SaveChanges();//在更新
                }
            }
        }
    }
}
