using System;
using System.Collections.Generic;
using Infrastructure.GameStateMachine.States;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Infrastructure.GameStateMachine
{
    public class StateMachine : IService
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine()
        {
            _states = new Dictionary<Type, IState>
            {
                { typeof(BootstrapState), new BootstrapState(this) },
            };
        }

        public void Enter<T>() where T : IState
        {
            _currentState?.Exit();
            IState state = _states[typeof(T)];
            _currentState = state;
            state.Enter();
        }
    }
}