using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Respository.Identity
{
    public class DefaultModuleRespository : BaseRespository<ModuleType, Guid, IDto<IDtoData>, IDtoData>
    {
        public DefaultModuleRespository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultModuleRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }

        public DefaultModuleRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto, string pageName, string actionName) : base(dbContext, moduleName, dtoData, dto, pageName, actionName)
        {
        }


        public override bool EntityValidate(ModuleType entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<ModuleType> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetItemAsync(ModuleType entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetListAsync(IEnumerable<ModuleType> entities)
        {
            throw new NotImplementedException();
        }
    }
}