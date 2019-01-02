using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Model
{
    public interface IDto<T> where T:IDtoData
    {
        IEnumerable<T> Datas{get;set;}
    }
}
