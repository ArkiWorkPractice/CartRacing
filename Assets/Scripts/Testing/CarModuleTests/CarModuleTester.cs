using CameraLogic;
using CarModule;
using Factories;
using ServiceLocatorModule;
using UnityEngine;
using UnityEngine.UI;

namespace Testing.CarModuleTests
{
    public class CarModuleTester : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Car carPrefab;
        [SerializeField] private Transform carSpawnPosition;
        [SerializeField] private Button damageCarButton;
        
        private Car _car;
        
        private void Awake()
        {
            CarFactory factory = new CarFactory(carPrefab);
            ServiceLocator.Instance.RegisterService(factory);

            _car = factory.Create(carSpawnPosition);
            cameraController.Follow(_car.transform);

            damageCarButton.onClick.AddListener(() => _car.MakeDamage(1));
        }
    }
}
