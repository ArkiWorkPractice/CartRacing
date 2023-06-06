using System.Threading.Tasks;
using EventBusModule;
using Factories;
using ServiceLocatorModule;
using Services;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IGameState
    {
        private EventBus _eventBus;

        public async Task Enter()
        {
            RegisterEventBus();
            RegisterFactories();
            RegisterLevelLoader();

            ScenesLoader loader = new ScenesLoader();
            await loader.LoadGame();

            await Task.CompletedTask;
        }

        private void RegisterEventBus()
        {
            _eventBus = new EventBus();
            ServiceLocator.Instance.RegisterService(_eventBus);
        }

        private void RegisterFactories()
        {
            PrefabsProvider prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            ConfigsProvider configsProvider = ServiceLocator.Instance.GetService<ConfigsProvider>();

            ObstacleFactory obstacleFactory = new ObstacleFactory(prefabsProvider.GetObstacles(),
                configsProvider.ObstacleSpawnerConfig.QuantityForEachObject);
            ServiceLocator.Instance.RegisterService(obstacleFactory);

            CarFactory carFactory = new CarFactory(prefabsProvider.GetCarPrefab());
            ServiceLocator.Instance.RegisterService(carFactory);
        }

        private void RegisterLevelLoader()
        {
            var levelLoader = new LevelLoader();
            ServiceLocator.Instance.RegisterService(levelLoader);
            _eventBus.Subscribe<SingleIntParameterEventBusArgs>(EventBusDefinitions.LoadLevelActionKey,
                levelLoader.LoadLevel);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.RestartGameActionKey, levelLoader.RestartLevel);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.LoadMainMenuActionKey, levelLoader.DestroyLevel);
        }

        public void Exit()
        {
        }
    }
}