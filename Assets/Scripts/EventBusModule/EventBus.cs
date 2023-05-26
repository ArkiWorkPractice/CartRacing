using ServiceLocatorModule.Interfaces;
using System.Collections.Generic;
using EventBusModule.Interfaces;

namespace EventBusModule
{
    public class EventBus : IService
    {
        private Dictionary<string, List<EventBusEventHandler>> _subscribersByEventName;

        public EventBus()
        {
            _subscribersByEventName = new Dictionary<string, List<EventBusEventHandler>>();
        }

        public void Subscribe<T>(string eventName, EventBusEventHandler action) where T : IEventBusEventArgs
        {
            if (!_subscribersByEventName.ContainsKey(eventName))
            {
                _subscribersByEventName.Add(eventName, new List<EventBusEventHandler>());
            }
            _subscribersByEventName[eventName].Add(action);
        }

        public void Unsubscribe<T>(string eventName, EventBusEventHandler action) where T : IEventBusEventArgs
        {
            if (_subscribersByEventName.ContainsKey(eventName))
            {
                _subscribersByEventName[eventName].Remove(action);
            }
        }

        public void Raise(string eventName, EventBusEventArgs arguments)
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