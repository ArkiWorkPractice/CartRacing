using CarModule;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Factories
{
    public class CarFactory : IService
    {
        private Car _car;
        private readonly Car _carPrefab;

        public Car GetCar => _car;
        
        public CarFactory(Car carPrefab)
        {
            _carPrefab = carPrefab;
        }

        public Car Create(Transform spawnPoint)
        {
            _car = Object.Instantiate(_carPrefab, spawnPoint);

            return _car;
        }
        
    }
}