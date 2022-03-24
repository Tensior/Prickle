using Meta;
using UnityWeld.Binding;
using Zenject;

namespace UI
{
    [Binding]
    public class PauseMenuVM : MonoBehaviourViewModel
    {
        private bool _isShown;
        
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Inject(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        [Binding]
        public bool IsShown
        {
            get => _isShown;
            set
            {
                if (value == _isShown)
                {
                    return;
                }
                
                _isShown = value;
                OnPropertyChanged(nameof(IsShown));
            }
        }

        [Binding]
        public async void ExitToMainMenu()
        {
            await _sceneLoader.LoadMainMenuAsync();
        }
    }
}