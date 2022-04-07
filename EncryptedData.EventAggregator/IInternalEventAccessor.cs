using System.Collections.Generic;

namespace EncryptedData.EventAggregator
{
    /// <summary>
    /// Only used internally.
    /// </summary>
    public interface IInternalEventAccessor
    {
        /// <summary>
        /// Sets the ThreadHandlers for the given event.
        /// </summary>
        /// <param name="handlers">The ThreadHandlers to set.</param>
        public void SetHandler(IReadOnlyDictionary<EventHandlerThread, IEventThreadHandler> handlers);
    }
}