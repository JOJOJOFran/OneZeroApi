using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Respository.Identity
{
    public class DefaultModulePermissionRespository : BaseRespository<ModulePermission, Guid, IDto<IDtoData>, IDtoData>
    {
        public DefaultModulePermissionRespository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultModulePermissionRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }

        public DefaultModulePermissionRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto, string pageName, string actionName) : base(dbContext, moduleName, dtoData, dto, pageName, actionName)
        {
        }

        public override  bool EntityValidate(ModulePermission entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<ModulePermission> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetItemAsync(ModulePermission entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetListAsync(IEnumerable<ModulePermission> entities)
        {
            throw new NotImplementedException();
        }
    }
}