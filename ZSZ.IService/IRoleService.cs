﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
   public interface IRoleService:IServiceSupport
    {
        RoleDTO[] GetAll();
        long GetTotalCount();
        RoleDTO GetById(long id);
        bool MarkDeleted(long id);
    }
}
