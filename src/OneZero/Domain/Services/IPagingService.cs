﻿using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Services
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPagingService
    {
        PageInfo Paging(int page, int limit, int sum,out int skipCount);
    }
}
