using System.Threading.Tasks;
using EventBusModule;
using ServiceLocatorModule;
using Services;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class LoadMainMenuState : IGameState
    {
        public async Task Enter()
        {
            var eventBus = ServiceLocator.Instance.GetService<EventBus>();
            var prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            
            UIController uiController = Object.Instantiate(prefabsProvider.GetUIController());
            Object.DontDestroyOnLoad(uiController);
            ServiceLocator.Instance.RegisterService(uiController);
            uiController.Initialize();
            eventBus.Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
            
            await Task.CompletedTask;
        }

        public void Exit()
        {
            
        }
    }
}