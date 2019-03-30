using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EventBues
{
    public interface IEventBus
    {
        //ConcurrentDictionary<Type, List<Type>> EventAndHandlerMapping { get; set; }

       void Subscribe<TEventData,TEventHandler>();

      // void Subscribe<TEventData>(TEventHandler )
    }
}
