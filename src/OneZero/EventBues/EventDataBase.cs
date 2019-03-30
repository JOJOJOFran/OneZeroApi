using OneZero.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EventBues
{
    public class EventDataBase : IEventData
    {
        public Guid Id { get; }
        public DateTime EventTime { get; }
        public object DataSource { get; set; }

        public EventDataBase()
        {
            Id = GuidHelper.NewGuid();
            EventTime = DateTime.Now;
        }
    }
}
