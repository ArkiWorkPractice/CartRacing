using EventBusModule;
using Infrastructure.StateMachine;
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

        private Game _game;

        private async void Awake()
        {
            DontDestroyOnLoad(this);
            
            RegisterServices();

            await _game.StartGame();
        }

        private void RegisterServices()
        {
            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabsProvider);

            _game = new Game();
            ServiceLocator.Instance.RegisterService(_game);
        }
    }
}