using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EncryptedData.EventAggregator.Wpf
{
    public class WpfEventThreadHandler : IEventThreadHandler
    {
        public Task Invoke(Task task)
        {
            return Application.Current.Dispatcher.InvokeAsync(() => task).Task.Unwrap();
        }
    }
}
