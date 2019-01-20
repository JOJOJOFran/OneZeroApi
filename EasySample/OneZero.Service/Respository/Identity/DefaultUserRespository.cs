using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneZero.Model;
using OneZero.Model.Identity;

namespace OneZero.Service.Repository.Identity
{
    public class DefaultUserRepository : BaseRepository<User, Guid>
    {
        public DefaultUserRepository(DbContext dbContext, DtoData dtoData,  Dto<DtoData> dto) : base(dbContext, dtoData, dto)
        {
        }

        public DefaultUserRepository(DbContext dbContext, string moduleName, DtoData dtoData,  Dto<DtoData> dto) : base(dbContext, moduleName, dtoData, dto)
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

        public override async Task<IEnumerable<DtoData>> GetItemAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<DtoData>> GetListAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
