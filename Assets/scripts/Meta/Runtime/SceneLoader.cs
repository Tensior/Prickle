using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Meta
{
    public class SceneLoader : ISceneLoader
    {
        private const string LEVEL1 = "level1";
        private const string MAIN_MENU = "MainMenu";

        Task ISceneLoader.LoadLevelAsync() => LoadSceneAsync(LEVEL1);

        Task ISceneLoader.LoadMainMenuAsync() => LoadSceneAsync(MAIN_MENU);

        private async Task LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (asyncOperation is { isDone: false })
            {
                await Task.Yield();
            }
        }
    }
}
