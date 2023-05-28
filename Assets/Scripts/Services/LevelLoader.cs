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

        public void LoadLevel(int levelIndex)
        {
            ServiceLocator serviceLocator = ServiceLocator.Instance;
            Level level = _levelFactory.Create(serviceLocator.GetService<PrefabProvider>().GetLevel(levelIndex));
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
