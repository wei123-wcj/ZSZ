using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.Service
{
    public class PermissionService : IPermissionService
    {
       
        public PermissionDTO[] GetAll()
        {
            using (MyContext my=new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return bs.GetAll().AsNoTracking().ToList().Select(m => GetDTO(m)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return GetDTO(bs.GetById(id));
                //return GetAll().Where(m => m.Id == id).FirstOrDefault();
            }
           
        }

        public long GetTotalCount()
        {
            using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return bs.GetTotalCount();
            }
            //return GetAll().Length;
        }

        public bool MarkDeleted(long id)
        {
             using (MyContext my = new MyContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(my);
                return bs.MarkDeleted(id);
            }
        }
        public bool Insert(PermissionEntity permission)
        {
            using (MyContext my = new MyContext())
            {
                 my.Permissions.Add(permission);
                int b = my.SaveChanges();
                if(b>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //PermissionEntity转换PermissionDTO
        public PermissionDTO GetDTO(PermissionEntity p)
        {
            PermissionDTO d = new PermissionDTO
            {
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                CreateDateTime = p.CreateDateTime
            };
            return d;
        }
    }
}
