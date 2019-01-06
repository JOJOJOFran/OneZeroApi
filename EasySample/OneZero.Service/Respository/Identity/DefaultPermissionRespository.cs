using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Repository.Identity
{
    public class DefaultPermissionRepository : BaseRepository<PermissionType, Guid>
    {
        public DefaultPermissionRepository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultPermissionRepository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }


        public override bool EntityValidate(PermissionType entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<PermissionType> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetItemAsync(PermissionType entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetListAsync(IEnumerable<PermissionType> entities)
        {
            throw new NotImplementedException();
        }
    }
}