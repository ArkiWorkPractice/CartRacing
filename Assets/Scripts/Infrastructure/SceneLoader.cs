using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class ScenesLoader
    {
        private const string GameScene = "Game";

        public async Task LoadGame()
        {
            SceneManager.LoadScene(GameScene, LoadSceneMode.Additive);

            await Task.CompletedTask;
        }
    }
}