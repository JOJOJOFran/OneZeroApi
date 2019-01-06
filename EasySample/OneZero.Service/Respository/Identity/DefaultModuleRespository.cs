using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Repository.Identity
{
    public class DefaultModuleRepository : BaseRepository<ModuleType, Guid>
    {
        public DefaultModuleRepository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultModuleRepository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
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