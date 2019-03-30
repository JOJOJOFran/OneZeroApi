using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch
{
    /// <summary>
    /// 注入成单例的
    /// </summary>
    public class InMemoryDispatchQueueStore: IDispatchQueue
    {
        public IEnumerable<DispatchQueueData> dispatchQueue { get;  set; }

        public InMemoryDispatchQueueStore()
        {
            dispatchQueue = new ConcurrentQueue<DispatchQueueData>();
        }

        public void Enqueue(DispatchQueueData queueData)
        {
            ((ConcurrentQueue<DispatchQueueData>)dispatchQueue).Enqueue(queueData);
        }

        public DispatchQueueData Dequeue()
        {
            DispatchQueueData queueData;
            if (!((ConcurrentQueue<DispatchQueueData>)dispatchQueue).TryDequeue(out queueData))
                return null;
            return queueData;
        }

        public DispatchQueueData Peek()
        {
            DispatchQueueData queueData;
            if (!((ConcurrentQueue<DispatchQueueData>)dispatchQueue).TryPeek(out queueData))
                return null;
            return queueData;
        }

        public void Clear()
        {
            DispatchQueueData queueData = new DispatchQueueData();
            while (((ConcurrentQueue<DispatchQueueData>)dispatchQueue).Count >= 1)
            {
                ((ConcurrentQueue<DispatchQueueData>)dispatchQueue).TryDequeue(out queueData);
            }
        }
    }
}
