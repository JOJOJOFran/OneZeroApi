using OneZero.Domain.Audits;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OneZero.Domain.Repositories
{
    public interface IDbContext
    {
        //IDbActionAudit actionAudit { get; set; }

        int SaveChanges();

        Task<int> SaveChanges(CancellationToken token=default(CancellationToken));
    }
}
