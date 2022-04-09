using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Meta
{
    public class SceneLoader : ISceneLoader
    {
        public const string LEVEL1 = "level1";
        private const string MAIN_MENU = "MainMenu";
        
        private readonly IMusicController _musicController;

        public SceneLoader(IMusicController musicController)
        {
            _musicController = musicController;
        }

        async Task ISceneLoader.LoadLevelAsync()
        {
            _musicController.StopCurrentMusicAsync();
            await LoadSceneAsync(LEVEL1);
            _musicController.PlayLevelMusic(LEVEL1);
        }

        async Task ISceneLoader.LoadMainMenuAsync()
        {
            _musicController.StopCurrentMusicAsync();
            await LoadSceneAsync(MAIN_MENU);
            _musicController.PlayMainMenuMusic();
        }

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
