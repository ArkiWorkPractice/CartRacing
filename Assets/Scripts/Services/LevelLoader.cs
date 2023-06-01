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

        public void LoadLevel(int levelIndex)
        {
            Level level = _levelFactory.Create(_prefabsProvider.GetLevel(levelIndex));
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
