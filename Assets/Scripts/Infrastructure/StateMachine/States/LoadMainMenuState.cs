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
        private EventBus _eventBus;
        
        public async Task Enter()
        {
            _eventBus = ServiceLocator.Instance.GetService<EventBus>();

            GenerateUI();
            
            await Task.CompletedTask;
        }

        private void GenerateUI()
        {
            var prefabsProvider = ServiceLocator.Instance.GetService<PrefabsProvider>();
            
            UIController uiController = Object.Instantiate(prefabsProvider.GetUIController());
            ServiceLocator.Instance.RegisterService(uiController);
            uiController.Initialize();
            _eventBus.Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
        }
        
        public void Exit()
        {
            
        }
    }
}