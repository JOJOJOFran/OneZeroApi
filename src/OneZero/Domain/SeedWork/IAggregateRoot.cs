using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain
{
    public interface IAggregateRoot<out TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {

    }
}
