using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class ScenesLoader
    {
        private const string GameScene = "Game";

        public async Task LoadGame()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(GameScene, LoadSceneMode.Single);
            
            
            await Task.CompletedTask;
        }
    }
}