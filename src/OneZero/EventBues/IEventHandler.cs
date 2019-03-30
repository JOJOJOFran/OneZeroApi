using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneZero.EventBues
{
    /// <summary>
    /// 定义事件处理器公共接口，所有的事件处理都要实现该接口
    /// </summary>
    public interface IEventHandler
    {
    }

    /// <summary>
    /// 泛型事件处理器接口
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface IEventHandler<TEventData> : IEventHandler where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理器
        /// </summary>
        /// <param name="eventData"></param>
        void Handle(TEventData eventData);

        /// <summary>
        /// 异步事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        Task HandleAsync(TEventData eventData, CancellationToken cancelToken = default(CancellationToken));
    }
}
