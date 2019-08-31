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
                return ToDto(bs.GetById(id));
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
            RoleDTO roleDTO = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                CreateDateTime = role.CreateDateTime,
            };
            return roleDTO;
        }
    }
}
