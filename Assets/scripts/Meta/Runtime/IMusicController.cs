using System.Threading.Tasks;

namespace Meta
{
    public interface IMusicController
    {
        void PlayMainMenuMusic();
        void PlayLevelMusic(string levelName);
        Task StopCurrentMusicAsync();
    }
}