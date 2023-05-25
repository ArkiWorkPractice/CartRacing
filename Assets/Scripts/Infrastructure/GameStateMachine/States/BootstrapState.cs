using ServiceLocatorModule;
using Services.Input;

namespace Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private StateMachine _stateMachine;
        public BootstrapState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}