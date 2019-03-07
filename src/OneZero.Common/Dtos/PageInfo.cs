using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Dtos
{
    public class PageInfo
    {
        public int CurrentCount { get; set; }

        public int PageCount { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public int Sum { get; set; }
    }
}
