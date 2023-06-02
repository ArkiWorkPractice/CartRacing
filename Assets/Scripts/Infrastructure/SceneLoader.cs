using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class ScenesLoader
    {
        private const string GameScene = "Game";

        public void LoadGame()
        {
            SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
        }
    }
}