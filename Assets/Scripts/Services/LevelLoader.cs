using CameraLogic;
using CarModule;
using EventBusModule;
using EventBusModule.Interfaces;
using Factories;
using Levels;
using ObstacleSpawn;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;

namespace Services
{
    public class LevelLoader : IService
    {
        private readonly PrefabsProvider _prefabsProvider;
        private readonly LevelFactory _levelFactory;
        private readonly CarFactory _carFactory;
        private readonly ServiceLocator _serviceLocator;
        private readonly EventBus _eventBus;

        public LevelLoader()
        {
            _serviceLocator = ServiceLocator.Instance;
            _prefabsProvider = _serviceLocator.GetService<PrefabsProvider>();
            _carFactory = _serviceLocator.GetService<CarFactory>();
            _levelFactory = new LevelFactory();
            _eventBus = _serviceLocator.GetService<EventBus>();
        }

        public void LoadLevel(IEventBusArgs e)
        {
            Level level = _levelFactory.Create(_prefabsProvider.GetLevel(((SingleIntParameterEventBusArgs)e).Number));
            level.StartLevel();
            _eventBus.Raise(EventBusDefinitions.StartRaceActionKey, new EventBusArgs());
            Car car = _carFactory.Create(level.GetPlayerPosition());
            // car.InitializeCar();
            // _eventBus.Raise(EventBusDefinitions.UpdateHealthValueActionKey, new SingleIntParameterEventBusArgs(car.CurrentHealth));
            _serviceLocator.GetService<CameraController>().Follow(car.transform);
        }

        public void RestartLevel(IEventBusArgs e)
        {
            Level level = _levelFactory.CurrentLevel;
            _eventBus.Raise(EventBusDefinitions.StartRaceActionKey, new EventBusArgs());
            Car car = _carFactory.Create(level.GetPlayerPosition());
            _carFactory.GetCar.Reinitialize();
            // _eventBus.Raise(EventBusDefinitions.UpdateHealthValueActionKey, new SingleIntParameterEventBusArgs(car.CurrentHealth));
            _serviceLocator.GetService<CameraController>().Follow(car.transform);
        }

        public void DestroyLevel(IEventBusArgs e)
        {
            _levelFactory.Remove();
            _carFactory.Clear();
        }
    }
}
