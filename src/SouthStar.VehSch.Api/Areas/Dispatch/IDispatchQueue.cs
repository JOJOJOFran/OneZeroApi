using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch
{
    public interface IDispatchQueue
    {
        IEnumerable<DispatchQueueData> dispatchQueue { get;  set; }
        void Enqueue(DispatchQueueData queueData);
        DispatchQueueData Dequeue();
        DispatchQueueData Peek();
        void Clear();

    }
}
