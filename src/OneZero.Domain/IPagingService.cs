using OneZero.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain
{
    public interface IPagingService
    {
        PageInfo Paging(int page, int limit, int sum,out int skipCount);
    }
}
