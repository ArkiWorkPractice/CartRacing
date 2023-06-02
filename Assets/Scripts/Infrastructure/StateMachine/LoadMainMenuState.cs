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
            var eventBus = ServiceLocator.Instance.GetService<EventBus>();
            var prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            
            UIController uiController = Object.Instantiate(prefabsProvider.GetUIController());
            ServiceLocator.Instance.RegisterService(uiController);
            uiController.Initialize();
            eventBus.Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
            
            var levelLoader = new LevelLoader();
            eventBus.Subscribe<SingleIntParameterEventBusArgs>(EventBusDefinitions.LoadLevelActionKey, levelLoader.LoadLevel);
        }

        public void Exit()
        {
            
        }
    }
}