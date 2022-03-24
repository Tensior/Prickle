using Meta;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

namespace UI
{
    [Binding]
    public class MainMenuVM : MonoBehaviourViewModel
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Initialize(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        [Binding]
        public async void StartGame()
        {
            await _sceneLoader.LoadLevelAsync();
        }
        
        [Binding]
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}