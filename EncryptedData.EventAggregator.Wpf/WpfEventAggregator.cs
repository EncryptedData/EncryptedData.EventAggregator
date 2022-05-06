namespace EncryptedData.EventAggregator.Wpf
{
    public class WpfEventAggregator : EventAggregator
    {
        public WpfEventAggregator() :
            base(new WpfEventThreadHandler())
        {
        }
    }
}