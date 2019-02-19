using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dtos;
using OneZero.Domain.Models;
using OneZero.Domain.Repositories;
using OneZero.EntityFrameworkCore.Repositories;

namespace OneZero.Application.Stores
{
    public class BaseStore<TEntity, TKey> : EFRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public BaseStore(OutputDto output, Logger<EFRepository<TEntity, TKey>> logger, IDbContext dbContext) : base(output, logger, dbContext)
        {
        }

        public override bool EntityValidate(TEntity entity, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override bool EntityValidate(IEnumerable<TEntity> entities, out string entityInfo)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<DataDto>> GetItemAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<DataDto>> GetListAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
