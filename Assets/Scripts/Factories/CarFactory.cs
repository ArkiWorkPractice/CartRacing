using CarModule;
using EventBusModule;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Factories
{
    public class CarFactory : IService
    {
        private readonly Car _carPrefab;
        
        private Car _car;
        private EventBus _eventBus;
        
        public Car GetCar => _car;
        
        public CarFactory(Car carPrefab)
        {
            _carPrefab = carPrefab;
            _eventBus = ServiceLocator.Instance.GetService<EventBus>();
        }

        public Car Create(Transform spawnPoint)
        {
            if (_car) return _car;
            
            _car = Object.Instantiate(_carPrefab, spawnPoint);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.EndGameActionKey, _car.StopCar);
            return _car;
        }

        public void Clear()
        {
            _eventBus.Unsubscribe<EventBusArgs>(EventBusDefinitions.EndGameActionKey, _car.StopCar);
            Object.Destroy(_car.gameObject);
            _car = null;
        }
        
    }
}