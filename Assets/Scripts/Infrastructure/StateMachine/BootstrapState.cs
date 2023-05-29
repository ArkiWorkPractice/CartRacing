using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IGameState
    {
        public void Enter()
        {
            SceneManager.LoadScene("Game");
        }

        public void Exit()
        {
            
        }
    }
}