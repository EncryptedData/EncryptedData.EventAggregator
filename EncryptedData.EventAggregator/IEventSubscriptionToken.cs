using System;

namespace EncryptedData.EventAggregator
{
    public interface IEventSubscriptionToken : IDisposable
    {
        /// <summary>
        /// The unique ID for this subscriber.
        /// </summary>
        Guid Guid { get; }
        
        /// <summary>
        /// Un-subscribe this handler from the Event
        /// </summary>
        void Unsubscribe();
    }
}