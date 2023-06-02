using EventBusModule;
using ServiceLocatorModule;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IGameState
    {
        public void Enter()
        {
            EventBus eventBus = new EventBus();

            ServiceLocator.Instance.RegisterService(eventBus);
        }

        public void Exit()
        {
        }
    }
}