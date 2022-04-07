using System.Threading.Tasks;

namespace EncryptedData.EventAggregator.ThreadHandlers
{
    public class BackgroundEventThreadHandler : IEventThreadHandler
    {
        /// <summary>
        /// Marshals the given Task onto the C# default background threads.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <returns>An awaitable task</returns>
        public Task Invoke(Task task)
        {
            return Task.Run(() => task);
        }
    }
}