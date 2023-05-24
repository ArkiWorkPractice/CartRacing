namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IState
    {
        private StateMachine _stateMachine;

        public LoadLevelState()
        {
            _stateMachine = new StateMachine();
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