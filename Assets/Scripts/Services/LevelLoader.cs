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

        public LevelLoader()
        {
            _prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            _levelFactory = new LevelFactory();
        }

        public void LoadLevel(IEventBusArgs args)
        {
            Level level = _levelFactory.Create(_prefabsProvider.GetLevel(((SingleIntParameterEventBusArgs)args).Number));
            level.StartLevel();
            /*
             * PlayerFactory = ServiceLocator;
             * PlayerFactory.Create(level.PlayerSpawnPosition);
             */
        }

        public void DestroyLevel()
        {
            _levelFactory.Remove();
        }
    }
}
