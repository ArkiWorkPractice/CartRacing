using EventBusModule;
using ServiceLocatorModule;
using UI;
using UnityEngine;

namespace Testing.UIModuleTests
{
    public class TemporaryBootstrap : MonoBehaviour
    {
        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = new EventBus();

            ServiceLocator.Instance.RegisterService(_eventBus);
        }

        private void Start()
        {
            ServiceLocator.Instance.GetService<UIController>().Initialize();
            ServiceLocator.Instance.GetService<EventBus>().Raise(EventBusDefinitions.StartGameActionKey, new EventBusArgs());
        }
    }
}