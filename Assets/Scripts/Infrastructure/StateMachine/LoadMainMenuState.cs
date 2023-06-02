using EventBusModule;
using ServiceLocatorModule;
using Services;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class LoadMainMenuState : IGameState
    {
        public void Enter()
        {
            var prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            UIController uiController = Object.Instantiate(prefabsProvider.GetUIController());
            uiController.Initialize();
            ServiceLocator.Instance.RegisterService(uiController);
            ServiceLocator.Instance.GetService<EventBus>().Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
        }

        public void Exit()
        {
            
        }
    }
}