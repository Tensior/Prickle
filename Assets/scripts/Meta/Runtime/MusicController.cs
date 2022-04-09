using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Meta
{
    public class MusicController : MonoBehaviour, IMusicController
    {
        [SerializeField] private AudioSource _mainMenuMusic;
        [SerializeField] private AudioSource _level1Music;
        [SerializeField, Range(0f, 1f)] private float _musicFadeOutVolume;
        [SerializeField] private float _musicFadeOutDuration = 0.5f;

        private Dictionary<string, AudioSource> _levelMusic;
        
        private AudioSource _currentMusicPlaying;

        private void Awake()
        {
            _levelMusic = new Dictionary<string, AudioSource>
            {
                { SceneLoader.LEVEL1, _level1Music }
            };
        }
        
        public void PlayMainMenuMusic()
        {
            PlayMusic(_mainMenuMusic);
        }

        public void PlayLevelMusic(string levelName)
        {
            PlayMusic(_levelMusic[levelName]);
        }

        public async Task StopCurrentMusicAsync()
        {
            if (_currentMusicPlaying != null)
            {
                var musicToStop = _currentMusicPlaying;
                var startVolume = musicToStop.volume;
                
                await FadeVolumeAsync(musicToStop, startVolume, _musicFadeOutVolume, _musicFadeOutDuration);
                
                musicToStop.Stop();
                musicToStop.volume = startVolume;
                
                if (_currentMusicPlaying == musicToStop)
                {
                    _currentMusicPlaying = null;
                }
            }
        }
        
        private void PlayMusic(AudioSource music)
        {
            if (_currentMusicPlaying == music)
            {
                return;
            }

            _currentMusicPlaying = music;
            _currentMusicPlaying.Play();
        }
        
        private async Task FadeVolumeAsync(AudioSource audioSource, float startVolume, float targetVolume, float durationSeconds)
        {
            var currentTime = 0f;
            
            while (currentTime < durationSeconds && audioSource.isPlaying)
            {
                await Task.Yield();
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / durationSeconds);
                currentTime += Time.deltaTime;
            }
        }
    }
}