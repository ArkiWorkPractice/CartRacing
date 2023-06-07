using System.Threading.Tasks;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using ServiceLocatorModule.Interfaces;

namespace Infrastructure
{
    public class Game : IService
    {
        private readonly GameStateMachine _stateMachine;

        public Game()
        {
            _stateMachine = new GameStateMachine();
        }

        public async Task StartGame()
        {
            await _stateMachine.EnterAsync<BootstrapState>();
            await _stateMachine.EnterAsync<LoadMainMenuState>();
        }
        
    }
}