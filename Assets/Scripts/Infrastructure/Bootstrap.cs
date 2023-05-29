using UnityEngine;

namespace Infrastructure
{
    [DefaultExecutionOrder(100)]
    public class Bootstrap : MonoBehaviour
    {
        private Game _game;
        
        private void Awake()
        {
            _game = new Game();
            
            DontDestroyOnLoad(this);
        }
    }
}