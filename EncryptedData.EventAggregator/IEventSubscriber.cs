using System.Threading.Tasks;

namespace EncryptedData.EventAggregator
{
    /// <summary>
    /// Used for internal purposes only. Do not use externally
    /// </summary>
    internal interface IEventSubscriber
    {
        /// <summary>
        /// Invoke the given task.
        /// </summary>
        /// <param name="param">The parameter for the function</param>
        /// <returns>The awaitable task.</returns>
        Task Invoke(object? param);
        
        /// <summary>
        /// Which thread does this handler want to be run on?
        /// </summary>
        EventHandlerThread EventHandlerThread { get; }
    }
}