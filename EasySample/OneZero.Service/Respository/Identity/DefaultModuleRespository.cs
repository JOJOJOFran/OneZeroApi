using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Model.Identity;
using OneZero.Model;

namespace OneZero.Service.Repository.Identity
{
    public class DefaultModuleRepository : BaseRepository<ModuleType, Guid>
    {
        public DefaultModuleRepository(DbContext dbContext, DtoData dtoData,  Dto<DtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultModuleRepository(DbContext dbContext, string moduleName, DtoData dtoData,  Dto<DtoData> dto) : base(dbContext, moduleName, dtoData, dto)
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

        public override async Task<IEnumerable<DtoData>> GetItemAsync(ModuleType entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<DtoData>> GetListAsync(IEnumerable<ModuleType> entities)
        {
            throw new NotImplementedException();
        }
    }
}