using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }
        
        public Game()
        {
            StateMachine = new GameStateMachine();
            StateMachine.Enter<BootstrapState>();
        }
    }
}