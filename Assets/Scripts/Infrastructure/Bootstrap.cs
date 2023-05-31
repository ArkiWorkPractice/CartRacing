using ServiceLocatorModule;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    [DefaultExecutionOrder(100)]
    public class Bootstrap : MonoBehaviour
    {
		[SerializeField] private ConfigsProvider configsProvider;
        [SerializeField] private PrefabProvider prefabProvider;
		
		private const string GameScene = "Game";
		
        private Game _game;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabProvider);
            
            _game = new Game();
            
            DontDestroyOnLoad(this);
            
            SceneManager.LoadScene(GameScene);
        }
    }
}