using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncryptedData.EventAggregator
{

    public class Event : IEvent, IInternalEventAccessor
    {
        public void Unsubscribe(IEventSubscriptionToken token)
        {
            lock (_mutex)
            {
                _subscribers.Remove(token.Guid);
            }
        }

        public Task Fire()
        {
            var subs = GetSubscribers();

            return Task.WhenAll(subs.Select(InvokeOnHandler));
        }

        public async void FireAndForget()
        {
            await Fire();
        }

        public IEventSubscriptionToken Subscribe(Func<Task> handler, EventHandlerThread handlerThread)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var subscriber = new EventSubscriber(handler, this, handlerThread);

            lock (_mutex)
            {
                _subscribers.Add(subscriber.Guid, subscriber);
            }

            return subscriber;
        }
        
        public void SetHandler(IReadOnlyDictionary<EventHandlerThread, IEventThreadHandler> handlers)
        {
            _handlers = handlers;
        }

        private IEnumerable<IEventSubscriber> GetSubscribers()
        {
            lock (_mutex)
            {
                return _subscribers.Select(e => e.Value).ToList();
            }
        }

        private Task InvokeOnHandler(IEventSubscriber subscriber)
        {
            return _handlers[subscriber.EventHandlerThread].Invoke(subscriber.Invoke(null));
        }

        private readonly object _mutex = new();
        private readonly Dictionary<Guid, IEventSubscriber> _subscribers = new();
        private IReadOnlyDictionary<EventHandlerThread, IEventThreadHandler> _handlers = null;
    }

    public class Event<T> : IEvent<T>, IInternalEventAccessor
    {
        public void Unsubscribe(IEventSubscriptionToken token)
        {
            lock (_mutex)
            {
                _subscribers.Remove(token.Guid);
            }
        }

        public Task Fire(T param)
        {
            var subs = GetSubscribers();

            return Task.WhenAll(subs.Select(e => InvokeOnHandler(e, param)));
        }

        public async void FireAndForget(T param)
        {
            await Fire(param);
        }

        public IEventSubscriptionToken Subscribe(Func<T, Task> handler, EventHandlerThread handlerThread)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var subscriber = new EventSubscriber<T>(handler, this, handlerThread);

            lock (_mutex)
            {
                _subscribers.Add(subscriber.Guid, subscriber);
            }

            return subscriber;
        }
        
        public void SetHandler(IReadOnlyDictionary<EventHandlerThread, IEventThreadHandler> handlers)
        {
            _handlers = handlers;
        }
        
        private IEnumerable<IEventSubscriber> GetSubscribers()
        {
            lock (_mutex)
            {
                return _subscribers.Select(e => e.Value).ToList();
            }
        }
        
        private Task InvokeOnHandler<T>(IEventSubscriber subscriber, T param)
        {
            return _handlers[subscriber.EventHandlerThread].Invoke(subscriber.Invoke(param));
        }
        
        private readonly object _mutex = new();
        private readonly Dictionary<Guid, IEventSubscriber> _subscribers = new();
        private IReadOnlyDictionary<EventHandlerThread, IEventThreadHandler> _handlers = null;
        
    }
}