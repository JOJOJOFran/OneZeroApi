using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Entity.Identity;
using OneZero.Model;

namespace OneZero.Service.Respository.Identity
{
    public class DefaultRoleRespository : BaseRespository<Role, Guid, IDto<IDtoData>, IDtoData>
    {
        public DefaultRoleRespository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultRoleRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }

        public DefaultRoleRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto, string pageName, string actionName) : base(dbContext, moduleName, dtoData, dto, pageName, actionName)
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
