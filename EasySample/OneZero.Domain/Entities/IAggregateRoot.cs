using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Entities
{
    public interface IAggregateRoot<out TKey> :IEntity<TKey>
    { 

    }
}
