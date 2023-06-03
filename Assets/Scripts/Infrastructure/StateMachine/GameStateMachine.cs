using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.StateMachine.States;
using UnityEditor;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _currentState;
        
        public GameStateMachine()
        {
            _states = new Dictionary<Type, IGameState>
            {
                { typeof(BootstrapState), new BootstrapState() },
                { typeof(LoadMainMenuState), new LoadMainMenuState() },
            };
        }

        public async Task EnterAsync<T>() where T : IGameState
        {
            _currentState?.Exit();

            _currentState = _states[typeof(T)];
            await _currentState.Enter();
        }
    }
}