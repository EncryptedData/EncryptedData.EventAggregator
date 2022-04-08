using System.Threading.Tasks;

namespace EncryptedData.EventAggregator
{
    public interface IEventThreadHandler
    {
        /// <summary>
        /// Handles scheduling and marshaling the given Task on the specified Thread.
        /// </summary>
        /// <param name="task">The Task to run on the specified Thread</param>
        /// <returns>An awaitable Task</returns>
        Task Invoke(Task task);
    }
}