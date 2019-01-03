using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Respository.Identity
{
    public class DefaultPermissionRespository : BaseRespository<PermissionType, Guid, IDto<IDtoData>, IDtoData>
    {
        public DefaultPermissionRespository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultPermissionRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }

        public DefaultPermissionRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto, string pageName, string actionName) : base(dbContext, moduleName, dtoData, dto, pageName, actionName)
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