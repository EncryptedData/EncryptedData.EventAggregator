using System.Threading.Tasks;

namespace EncryptedData.EventAggregator.ThreadHandlers
{
    public class DefaultEventThreadHandler : IEventThreadHandler
    {
        /// <summary>
        /// Default marshaller, which runs on the current thread.
        /// </summary>
        /// <param name="task">The Task to run.</param>
        /// <returns>An awaitable task.</returns>
        public Task Invoke(Task task)
        {
            return task;
        }
    }
}