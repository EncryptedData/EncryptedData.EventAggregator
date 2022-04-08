namespace EncryptedData.EventAggregator
{
    public interface IEventAggregator
    {
        /// <summary>
        /// Gets the Event you wish to interact with.
        /// </summary>
        /// <typeparam name="TEventType">The Event type you wish to fire/subscribe to.</typeparam>
        /// <returns>The Event instance you want.</returns>
        TEventType GetEvent<TEventType>() where TEventType : IBaseEvent;
    }
}