using CameraLogic;
using CarModule.CarControl;
using Factories;
using ServiceLocatorModule;
using UnityEngine;

namespace CarModule
{
    public class CarModuleTester : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        
        [SerializeField] private Car carPrefab;
        [SerializeField] private Transform carSpawnPosition;
        private Car _car;
        
        void Awake()
        {
            CarFactory factory = new CarFactory(carPrefab);
            ServiceLocator.Instance.RegisterService(factory);

            _car = factory.Create(carSpawnPosition);
            cameraController.Follow(_car.transform);
        }

    }
}
