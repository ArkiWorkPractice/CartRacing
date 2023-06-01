using System;
using System.Collections.Generic;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IGameState> _states;
        private IGameState _currentState;
        
        public GameStateMachine()
        {
            _states = new Dictionary<Type, IGameState>
            {
                { typeof(BootstrapState), new BootstrapState() }
            };
        }

        public void Enter<T>() where T : IGameState
        {
            _currentState?.Exit();

            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }
    }
}