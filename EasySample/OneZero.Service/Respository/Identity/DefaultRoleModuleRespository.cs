using System;
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
    public class DefaultRoleModuleRepository : BaseRepository<RoleModule, Guid>
    {
        public DefaultRoleModuleRepository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultRoleModuleRepository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }


        public override bool EntityValidate(RoleModule entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<RoleModule> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetItemAsync(RoleModule entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetListAsync(IEnumerable<RoleModule> entities)
        {
            throw new NotImplementedException();
        }
    }
}