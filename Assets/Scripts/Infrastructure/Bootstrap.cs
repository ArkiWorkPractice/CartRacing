using Infrastructure.GameStateMachine;
using ServiceLocatorModule;
using UnityEngine;

namespace Infrastructure
{
    [DefaultExecutionOrder(100)]
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var stateMachine = new StateMachine();
            ServiceLocator.Instance.RegisterService(stateMachine);
        }
    }
}