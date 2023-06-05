using CameraLogic;
using EventBusModule;
using EventBusModule.Interfaces;
using Factories;
using ServiceLocatorModule;
using Services;
using UI;
using UnityEngine;

namespace Testing.LevelDesignModuleTests
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private ConfigsProvider configsProvider;
        [SerializeField] private PrefabsProvider prefabsProvider;
        [SerializeField] private int levelNumber;
        [SerializeField] private CarModule.Car carPrefab;
        [SerializeField] private UIController uiController;
        [SerializeField] private CameraController cameraController;

        private EventBus _eventBus;
        private LevelLoader _levelLoader;

        private void Awake()
        {
            _eventBus = new EventBus();
            ServiceLocator.Instance.RegisterService(_eventBus);
            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabsProvider);
            CarFactory factory = new CarFactory(carPrefab);
            ServiceLocator.Instance.RegisterService(factory);
            ServiceLocator.Instance.RegisterService(new ObstacleFactory(prefabsProvider.GetObstacles(), configsProvider.ObstacleSpawnerConfig.QuantityForEachObject));

            _levelLoader = new LevelLoader();
            ServiceLocator.Instance.RegisterService(_levelLoader);

            uiController.Initialize();
            _eventBus.Subscribe<SingleIntParameterEventBusArgs>(EventBusDefinitions.LoadLevelActionKey, _levelLoader.LoadLevel);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.RestartGameActionKey, _levelLoader.RestartLevel);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.LoadMainMenuActionKey, _levelLoader.DestroyLevel);
            _eventBus.Raise(EventBusDefinitions.StartGameActionKey, new EventBusArgs());
        }
    }
}