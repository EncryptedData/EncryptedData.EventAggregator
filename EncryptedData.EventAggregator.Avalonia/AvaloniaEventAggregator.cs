namespace EncryptedData.EventAggregator.Avalonia
{
    public class AvaloniaEventAggregator : EventAggregator
    {
        public AvaloniaEventAggregator() :
            base()
        {
            SetHandler(EventHandlerThread.UI, new AvaloniaThreadHandler());
        }
    }
}