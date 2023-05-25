using CarModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Factories
{
    public class CarFactory : IService
    {
        private readonly Car _carPrefab;
        
        private Car _car;
        
        public Car GetCar => _car;
        
        public CarFactory(Car carPrefab)
        {
            _carPrefab = carPrefab;
        }

        public Car Create(Transform spawnPoint)
        {
            if (_car) return _car;
            
            _car = Object.Instantiate(_carPrefab, spawnPoint);

            return _car;
        }
        
    }
}