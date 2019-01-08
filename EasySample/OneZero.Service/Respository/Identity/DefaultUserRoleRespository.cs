using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Repository.Identity
{
    public class DefaultUserRoleRepository : BaseRepository<UserRole, Guid>
    {
        public DefaultUserRoleRepository(DbContext dbContext, DtoData dtoData,  Dto<DtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultUserRoleRepository(DbContext dbContext, string moduleName, DtoData dtoData,  Dto<DtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }


        public override bool EntityValidate(UserRole entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<UserRole> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<DtoData>> GetItemAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<DtoData>> GetListAsync(IEnumerable<UserRole> entities)
        {
            throw new NotImplementedException();
        }
    }
}