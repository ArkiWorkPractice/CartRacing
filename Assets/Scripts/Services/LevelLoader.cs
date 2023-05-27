using Factories;
using Levels;
using ObstacleSpawn;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;

namespace Services
{
    public class LevelLoader : IService
    {
        private readonly PrefabProvider _prefabProvider;

        private readonly LevelFactory _levelFactory;

        public LevelLoader()
        {
            _prefabProvider = ServiceLocator.Instance.GetService<PrefabProvider>();
            _levelFactory = new LevelFactory();
        }

        public void OnLevelSelected(int levelId)
        {
            _levelFactory.Create(_prefabProvider.GetLevels()[levelId]);
            
        }
        public void EnterLoadLevelState(int levelIndex)
        {
            ServiceLocator serviceLocator = ServiceLocator.Instance;
            Level level = _levelFactory.Create(serviceLocator.GetService<PrefabProvider>().GetLevel(levelIndex));
            ConfigsProvider cfgProvider = serviceLocator.GetService<ConfigsProvider>();
            ObstacleSpawner obstacleSpawner = new ObstacleSpawner(level.GetSpawnPoints(), cfgProvider.ObstacleSpawnerConfig.QuantityForEachObject);
            /*
             * PlayerFactory = ServiceLocator;
             * PlayerFactory.Create(level.PlayerSpawnPosition);
             */
        }
    }
}
