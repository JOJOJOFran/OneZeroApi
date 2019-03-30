using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EventBues
{
    public class InMemoryEventStore : IEventStore
    {
        public ConcurrentDictionary<Type, List<Type>> EventHandlerMap { get; set; }
        IDictionary<Type, List<Type>> IEventStore.EventHandlerMap { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler, new()
        {
            throw new NotImplementedException();
        }

        public void Add(Type eventType, IEventHandler eventHandler)
        {
            throw new NotImplementedException();
        }

        public void Remove<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public void Remove(Type eventType, IEventHandler eventHandler)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(Type eventType)
        {
            throw new NotImplementedException();
        }
    }
}
