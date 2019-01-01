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
    public class DefaultRoleRespository : IRoleRespository
    {
        public DbContext Context => throw new NotImplementedException();

        public IQueryable<Role> Entities => throw new NotImplementedException();

        public IQueryable<Role> EntitieWithTracking => throw new NotImplementedException();

        public Task<int> AddAndGetCountAsync(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> AddRangeAsync(IEnumerable<Role> entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> DeleteAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> DeleteAsync(Expression<Func<Role, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> DeleteRangeAsync(IEnumerable<Guid> keys)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetDeleteCountAsync(IEnumerable<Guid> keys)
        {
            throw new NotImplementedException();
        }

        public Role GetItemAsync(Func<Role, bool> whereFunc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetListAsync(Func<Role, bool> whereFunc)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUpdateCountAsync(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> RecycleAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDto> UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> UpdateAsync<TInputDto>(TInputDto inputDto, Func<TInputDto, Role, Role> convertExpression, Guid key)
        {
            throw new NotImplementedException();
        }
    }
}
