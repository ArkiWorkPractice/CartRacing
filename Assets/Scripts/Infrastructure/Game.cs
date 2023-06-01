﻿using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game()
        {
            _stateMachine = new GameStateMachine();
        }

        public void Start()
        {
            _stateMachine.Enter<BootstrapState>();
        }
    }
}