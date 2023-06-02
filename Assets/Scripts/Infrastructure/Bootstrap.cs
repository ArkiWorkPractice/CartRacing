using EventBusModule;
using ServiceLocatorModule;
using Services;
using UnityEngine;

namespace Infrastructure
{
    [DefaultExecutionOrder(100)]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ConfigsProvider configsProvider;
        [SerializeField] private PrefabsProvider prefabsProvider;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            RegisterServices();
            
            new ScenesLoader().LoadGame();
        }

        private void RegisterServices()
        {
            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabsProvider);
            ServiceLocator.Instance.RegisterService(new EventBus());
        }
    }
}