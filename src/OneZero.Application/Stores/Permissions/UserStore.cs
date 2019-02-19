using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using OneZero.Application.Models.Permissions;
using OneZero.Common.Dtos;
using OneZero.Domain.Repositories;
using OneZero.EntityFrameworkCore.Repositories;

namespace OneZero.Application.Stores.Permissions
{
    public class UserStore : BaseStore<User, Guid>
    {
        public UserStore(OutputDto output, Logger<EFRepository<User, Guid>> logger, IDbContext dbContext) : base(output, logger, dbContext)
        {
        }
    }
}
