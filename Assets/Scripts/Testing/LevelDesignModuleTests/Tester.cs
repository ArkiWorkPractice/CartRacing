using Factories;
using ServiceLocatorModule;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Testing.LevelDesignModuleTests
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private Button button;
    
        [SerializeField] private ConfigsProvider configsProvider;
        [SerializeField] private PrefabsProvider prefabsProvider;
        private LevelLoader _level;
        private ObstacleFactory _obstacleFactory;
        [SerializeField] private int levelNumber;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabsProvider);
            ServiceLocator.Instance.RegisterService(new ObstacleFactory(prefabsProvider.GetObstacles(),configsProvider.ObstacleSpawnerConfig.QuantityForEachObject));
        }

        public void Start()
        {
            _level = new LevelLoader();
            button.onClick.AddListener(() => _level.LoadLevel(levelNumber));
        }

        public void EnterBootstrapState()
        {
            ServiceLocator.Instance.RegisterService(new LevelLoader());
        }
    }
}
