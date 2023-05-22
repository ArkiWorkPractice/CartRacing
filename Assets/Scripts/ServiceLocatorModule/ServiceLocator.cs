using System;
using System.Collections.Generic;
using ServiceLocatorModule.Interfaces;

namespace ServiceLocatorModule
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance { get; } = new ServiceLocator();
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public T GetService<T>() where T : class, IService
        {
            if (_services.TryGetValue(typeof(T), out var instance))
            {
                return (T)instance;
            }

            return null;
        }

        public void RegisterService<T>(T service) where T : class, IService
        {
            _services.TryAdd(typeof(T), service);
        }
    }
}