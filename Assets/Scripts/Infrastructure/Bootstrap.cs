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
        [SerializeField] private PrefabsProvider prefabsProvider;

        [SerializeField] private string gameScene;

        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            ServiceLocator.Instance.RegisterService(configsProvider);
            ServiceLocator.Instance.RegisterService(prefabsProvider);

            _game = new Game();

            SceneManager.LoadScene(gameScene);
        }
    }
}