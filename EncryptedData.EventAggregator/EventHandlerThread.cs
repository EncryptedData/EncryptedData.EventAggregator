namespace EncryptedData.EventAggregator
{
    public enum EventHandlerThread
    {
        /// <summary>
        /// The default option. Usually the current thread
        /// </summary>
        Default,
        
        /// <summary>
        /// If you are using a UI based framework such as WPF or Avalonia, then this on that UI thread.
        /// </summary>
        UI,
        
        /// <summary>
        /// Generic Background thread.
        /// </summary>
        Background
    }
}