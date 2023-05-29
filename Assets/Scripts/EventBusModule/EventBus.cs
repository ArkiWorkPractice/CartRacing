using ServiceLocatorModule.Interfaces;
using System.Collections.Generic;
using EventBusModule.Interfaces;

namespace EventBusModule
{
    public class EventBus : IService
    {
        private Dictionary<string, List<EventBusHandler>> _subscribersByEventName;

        public EventBus()
        {
            _subscribersByEventName = new Dictionary<string, List<EventBusHandler>>();
        }

        public void Subscribe<T>(string eventName, EventBusHandler action) where T : IEventBusArgs
        {
            if (!_subscribersByEventName.ContainsKey(eventName))
            {
                _subscribersByEventName.Add(eventName, new List<EventBusHandler>());
            }
            _subscribersByEventName[eventName].Add(action);
        }

        public void Unsubscribe<T>(string eventName, EventBusHandler action) where T : IEventBusArgs
        {
            if (_subscribersByEventName.ContainsKey(eventName))
            {
                _subscribersByEventName[eventName].Remove(action);
            }
        }

        public void Raise(string eventName, IEventBusArgs arguments)
        {
            if (_subscribersByEventName.TryGetValue(eventName, out var subscribers))
            {
                for (int i = 0; i < subscribers.Count; i++)
                {
                    subscribers[i](arguments);
                }
            }
        }
    }
}