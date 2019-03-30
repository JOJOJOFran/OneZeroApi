using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OneZero.EventBues
{
    public interface IEventStore
    {
        /// <summary>
        /// 将事件源类型与事件处理类型添加到存储，这里使用的是类型，应当使用即时的处理器工厂来存储
        /// </summary>
        /// <typeparam name="TEventData">事件源数据类型</typeparam>
        /// <typeparam name="TEventHandler">数据处理器类型</typeparam>
        void Add<TEventData, TEventHandler>() where TEventData : IEventData where TEventHandler : IEventHandler, new();

        /// <summary>
        /// 将事件源类型与事件处理器实例添加到存储，这里使用的是处理器实例，应当使用单例的处理器工厂来存储
        /// </summary>
        /// <param name="eventType">事件源类型</param>
        /// <param name="eventHandler">事件处理器实例</param>
        void Add(Type eventType, IEventHandler eventHandler);


        /// <summary>
        /// 移除指定事件源的处理委托实现
        /// </summary>
        /// <typeparam name="TEventData">事件源类型</typeparam>
        /// <param name="action">事件处理委托</param>
        void Remove<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// 移除指定事件源与处理器实例
        /// </summary>
        /// <param name="eventType">事件源类型</param>
        /// <param name="eventHandler">处理器实例</param>
        void Remove(Type eventType, IEventHandler eventHandler);

        /// <summary>
        /// 移除指定事件源的所有处理器
        /// </summary>
        /// <param name="eventType">事件源类型</param>
        void RemoveAll(Type eventType);


        /// <summary>
        /// 根据事件数据类型存储所有Handler
        /// </summary>
        IDictionary<Type, List<Type>> EventHandlerMap { get; set; }
    }
}
