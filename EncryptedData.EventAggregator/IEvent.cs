using System;
using System.Threading.Tasks;

namespace EncryptedData.EventAggregator
{
    /// <summary>
    /// Used for internal purposes. Do not extend/implement this yourself.
    /// </summary>
    public interface IBaseEvent
    {
        /// <summary>
        /// Handles unsubscribing a given Event subscription.
        /// This is thread safe.
        /// </summary>
        /// <param name="token">The event subscription to unsubscribe (stop receiving events).</param>
        void Unsubscribe(IEventSubscriptionToken token);
    }
    
    public interface IEvent : IBaseEvent
    {
        /// <summary>
        /// Fires an event that is handled by all active registered subscribers.
        /// </summary>
        /// <returns>A task to await on.</returns>
        Task Fire();

        /// <summary>
        /// Handles firing the event that is handled by all active registered subscribers, while also not blocking
        /// this current thread. Prefer awaiting Fire() instead.
        /// </summary>
        void FireAndForget();

        /// <summary>
        /// Subscribes (registers) a function to be handled when an event fires.
        /// </summary>
        /// <param name="handler">The function that is called when the event fires.</param>
        /// <param name="handlerThread">Which thread do you wish the function to be handled on?</param>
        /// <returns>A token which can be used to un-subscribe (un-register) the handler function.</returns>
        IEventSubscriptionToken Subscribe(Func<Task> handler, EventHandlerThread handlerThread);
    }

    public interface IEvent<T> : IBaseEvent
    {
        /// <summary>
        /// Fires an event that is handled by all active registered subscribers.
        /// </summary>
        /// <returns>A task to await on.</returns>
        Task Fire(T param);

        /// <summary>
        /// Handles firing the event that is handled by all active registered subscribers, while also not blocking
        /// this current thread. Prefer awaiting Fire() instead.
        /// </summary>
        void FireAndForget(T param);

        /// <summary>
        /// Subscribes (registers) a function to be handled when an event fires.
        /// </summary>
        /// <param name="handler">The function that is called when the event fires.</param>
        /// <param name="handlerThread">Which thread do you wish the function to be handled on?</param>
        /// <returns>A token which can be used to un-subscribe (un-register) the handler function.</returns>
        IEventSubscriptionToken Subscribe(Func<T, Task> handler, EventHandlerThread handlerThread);
    }
}