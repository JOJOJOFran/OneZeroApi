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
    public class DefaultUserRespository : IUserRespository
    {
        public DbContext Context => throw new NotImplementedException();

        public IQueryable<User> Entities => throw new NotImplementedException();

        public IQueryable<User> EntitieWithTracking => throw new NotImplementedException();

        public Task<int> AddAndGetCountAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> AddRangeAsync(IEnumerable<User> entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> DeleteAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> DeleteAsync(Expression<Func<User, bool>> predicate)
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

        public User GetItemAsync(Func<User, bool> whereFunc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetListAsync(Func<User, bool> whereFunc)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUpdateCountAsync(IEnumerable<User> entities)
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

        public Task<IDto> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDto> UpdateAsync<TInputDto>(TInputDto inputDto, Func<TInputDto, User, User> convertExpression, Guid key)
        {
            throw new NotImplementedException();
        }
    }
}
