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
    public class DefaultUserRespository : BaseRespository<User, Guid, IDto<IDtoData>, IDtoData>
    {
        public DefaultUserRespository(DbContext dbContext, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultUserRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto) : base(dbContext, moduleName, dtoData, dto)
        {
        }

        public DefaultUserRespository(DbContext dbContext, string moduleName, IDtoData dtoData, IDto<IDtoData> dto, string pageName, string actionName) : base(dbContext, moduleName, dtoData, dto, pageName, actionName)
        {
        }

         public override bool EntityValidate(User entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<User> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetItemAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IDtoData>> GetListAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
