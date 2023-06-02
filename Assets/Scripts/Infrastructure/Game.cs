using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure
{
    public class Game : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new GameStateMachine();
            _stateMachine.Enter<BootstrapState>();
            _stateMachine.Enter<LoadMainMenuState>();
        }
    }
}