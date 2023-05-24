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
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}