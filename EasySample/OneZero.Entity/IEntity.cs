using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Lib.Entity
{
    public interface IEntity<out TKey> where TKey:IEquatable<TKey>
    {
    }
}
