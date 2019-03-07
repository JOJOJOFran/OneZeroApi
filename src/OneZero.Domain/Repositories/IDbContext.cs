using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneZero.Domain.Repositories
{
    public interface IDbContext:IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token = default(CancellationToken));

  
    }
}
