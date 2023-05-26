using ServiceLocatorModule;
using EventBusModule;
using UnityEngine;
using UI;

namespace Assets.Scripts
{
    public class TemporaryBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var serviceLocator = ServiceLocator.Instance;
            serviceLocator.RegisterService(new EventBus());
        }

        private void Start()
        {
            ServiceLocator.Instance.GetService<UIController>().Initialize();
            ServiceLocator.Instance.GetService<EventBus>().Raise("OnStartGame", new EventBusEventArgs());
        }
    }
}