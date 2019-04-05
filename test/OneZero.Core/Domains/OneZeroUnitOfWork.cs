using OneZero.Common.Qiniu;
using OneZero.Domain;
using OneZero.EntityFrameworkCore.Domain;
using OneZero.EntityFrameworkCore.Extensions;
using OneZero.Enums;
using OneZero.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Domains
{
    public class OneZeroUnitOfWork : UnitOfWork
    {
        private readonly OneZeroContext _oneZeroContext;

        public OneZeroUnitOfWork(IServiceProvider provider, OneZeroContext  oneZeroContext) : base(provider)
        {
            _oneZeroContext = oneZeroContext;
        }

        public override IRepository<TEntity, Guid> Repository<TEntity, Guid>()
        {
            if (repositories == null)
                repositories = new Hashtable();

            var entityType = typeof(TEntity);
            try
            {
                if (!repositories.ContainsKey(entityType.Name))
                {
                    var baseType = typeof(OneZeroRepository<>);

                    var repositoryInstance = Activator.CreateInstance(baseType.MakeGenericType(entityType), DbContext, _provider.GetLogger<Repository<TEntity,Guid>>(),_oneZeroContext);

                    repositories.Add(entityType.Name, repositoryInstance);
                }
            }
            catch (Exception e)
            {
                throw new OneZeroException("UnitOfWork构建仓储示例失败", e, ResponseCode.Error);
            }

            return (IRepository<TEntity, Guid>)repositories[entityType.Name];
        }
    } 
}
