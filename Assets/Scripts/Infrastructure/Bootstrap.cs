using ServiceLocatorModule;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    [DefaultExecutionOrder(100)]
    public class Bootstrap : MonoBehaviour
    {
        private const string GameScene = "Game";
         
        [SerializeField] private ConfigsProvider configsProvider;
        [SerializeField] private PrefabProvider prefabProvider;
        
        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabProvider);
            
            SceneManager.LoadScene(GameScene);
        }
    }
}