﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Repository.Identity
{
    public class DefaultRoleRepository : BaseRepository<Role, Guid>
    {
        public DefaultRoleRepository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultRoleRepository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }



        public override bool EntityValidate(Role entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<Role> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetItemAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetListAsync(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }
    }
}
