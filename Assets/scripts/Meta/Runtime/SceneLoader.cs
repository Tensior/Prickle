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

        Task ISceneLoader.LoadLevelAsync()
        {
            _musicController.StopCurrentMusicAsync();
            _musicController.PlayLevelMusic(LEVEL1);
            return LoadSceneAsync(LEVEL1);
        }

        Task ISceneLoader.LoadMainMenuAsync()
        {
            _musicController.StopCurrentMusicAsync();
            _musicController.PlayMainMenuMusic();
            return LoadSceneAsync(MAIN_MENU);
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
