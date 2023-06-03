using System.Threading.Tasks;
using EventBusModule;
using Factories;
using ServiceLocatorModule;
using Services;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IGameState
    {
        public async Task Enter()
        {
            var eventBus = new EventBus();
            ServiceLocator.Instance.RegisterService(eventBus);
            
            var levelLoader = new LevelLoader();
            ServiceLocator.Instance.RegisterService(levelLoader);
            eventBus.Subscribe<SingleIntParameterEventBusArgs>(EventBusDefinitions.LoadLevelActionKey, levelLoader.LoadLevel);

            PrefabsProvider prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            ConfigsProvider configsProvider = ServiceLocator.Instance.GetService<ConfigsProvider>();
            ObstacleFactory obstacleFactory = new ObstacleFactory(prefabsProvider.GetObstacles(), configsProvider.ObstacleSpawnerConfig.QuantityForEachObject);
            ServiceLocator.Instance.RegisterService(obstacleFactory);
            
            ScenesLoader loader = new ScenesLoader();
            await loader.LoadGame();
        }

        public void Exit()
        {
        }
    }
}