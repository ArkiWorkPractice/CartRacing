using System;
using CameraLogic;
using CarModule;
using EventBusModule;
using Factories;
using ServiceLocatorModule;
using TMPro;
using UI;
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

        private EventBus _eventBus;
        private Car _car;
        
        private void Awake()
        {
            _eventBus = new EventBus();
            ServiceLocator.Instance.RegisterService(_eventBus);
            CarFactory factory = new CarFactory(carPrefab);
            ServiceLocator.Instance.RegisterService(factory);
            
            _car = factory.Create(carSpawnPosition);
            _car.InitializeCar();
            
            cameraController.Follow(_car.transform);

            damageCarButton.onClick.AddListener(() => _car.Reinitialize());
            damageCarButton.onClick.AddListener(() => _eventBus.Raise(EventBusDefinitions.StartRaceActionKey, new EventBusArgs()));
        }

        private void Start()
        {
            ServiceLocator.Instance.GetService<UIController>().Initialize();
            _eventBus.Raise(EventBusDefinitions.StartRaceActionKey,new EventBusArgs());
        }
    }
}
