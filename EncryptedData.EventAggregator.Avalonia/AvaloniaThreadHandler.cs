using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;

namespace EncryptedData.EventAggregator.Avalonia
{
    public class AvaloniaThreadHandler : IEventThreadHandler
    {
        public Task Invoke(Task task)
        {
            return Dispatcher.UIThread.InvokeAsync(() => task);
        }
    }
}