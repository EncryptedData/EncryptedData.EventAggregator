using System;
using System.Threading.Tasks;

namespace EncryptedData.EventAggregator
{
    internal class EventSubscriber : IEventSubscriber, IEventSubscriptionToken, IDisposable
    {
        internal EventSubscriber(Func<Task> func, IBaseEvent e, EventHandlerThread eventHandlerThread)
        {
            _func = func;
            _event = e;
            Guid = Guid.NewGuid();
            EventHandlerThread = eventHandlerThread;
        }

        public Task Invoke(object? _)
        {
            return _func();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        public Guid Guid { get; }
        
        public EventHandlerThread EventHandlerThread { get; }
        
        public void Unsubscribe()
        {
            _event.Unsubscribe(this);
        }

        private Func<Task> _func;
        private IBaseEvent _event;
    }

    internal class EventSubscriber<T> : IEventSubscriber, IEventSubscriptionToken, IDisposable
    {
        internal EventSubscriber(Func<T, Task> func, IBaseEvent e, EventHandlerThread eventHandlerThread)
        {
            _func = func;
            _event = e;
            Guid = Guid.NewGuid();
            EventHandlerThread = eventHandlerThread;
        }
        
        public Task Invoke(object? param)
        {
            return _func((T) param);
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        public Guid Guid { get; }

        public EventHandlerThread EventHandlerThread { get; }

        public void Unsubscribe()
        {
            _event.Unsubscribe(this);
        }

        private Func<T, Task> _func;
        private IBaseEvent _event;
    }
}