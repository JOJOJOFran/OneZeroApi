using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EventBues
{
    public interface IEventData
    {
        /// <summary>
        /// 事件编号
        /// </summary>
        Guid Id { get; }

        DateTime EventTime { get; }

        Object DataSource { get; }
    }
}
