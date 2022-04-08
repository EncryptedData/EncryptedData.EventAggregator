using System;
using System.Collections.Generic;
using EncryptedData.EventAggregator.ThreadHandlers;

namespace EncryptedData.EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        public EventAggregator()
        {
            _handlers.Add(EventHandlerThread.Default, new DefaultEventThreadHandler());
            _handlers.Add(EventHandlerThread.UI, new DefaultEventThreadHandler());
            _handlers.Add(EventHandlerThread.Background, new BackgroundEventThreadHandler());
        }
        
        public TEventType GetEvent<TEventType>() where TEventType : IBaseEvent
        {
            Type eventType = typeof(TEventType);

            lock (_mutex)
            {
                if (!_events.ContainsKey(eventType))
                {
                    TEventType e = Activator.CreateInstance<TEventType>();

                    ((IInternalEventAccessor) e).SetHandler(_handlers);

                    _events.Add(eventType, e);
                }

                return (TEventType)_events[eventType];
            }
        }

        public void SetHandler(EventHandlerThread eventHandlerThread, IEventThreadHandler handler)
        {
            _handlers.Add(eventHandlerThread, handler);
        }

        public IReadOnlyDictionary<EventHandlerThread, IEventThreadHandler> EventThreadHandlers => _handlers;

        private readonly object _mutex = new();
        private readonly Dictionary<Type, IBaseEvent> _events = new();
        private readonly Dictionary<EventHandlerThread, IEventThreadHandler> _handlers = new();
        
    }
}