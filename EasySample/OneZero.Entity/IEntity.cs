using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity
{
    public interface IEntity<out TKey> where TKey:IEquatable<TKey>
    {
    }
}
