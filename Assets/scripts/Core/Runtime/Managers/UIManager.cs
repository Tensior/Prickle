using Core.Interfaces;
using UI;
using UnityEngine;
using Zenject;

namespace Core.Managers
{
    public class UIManager : MonoBehaviour
    {
        private IPauseController _pauseController;
        private IInputProvider _inputProvider;
        private PauseMenuVM _pauseMenuVM;
        
        [Inject]
        public void Inject(IPauseController pauseController, IInputProvider inputProvider, PauseMenuVM pauseMenuVM)
        {
            _pauseController = pauseController;
            _inputProvider = inputProvider;
            _pauseMenuVM = pauseMenuVM;
        }

        private void Update()
        {
            if (!_inputProvider.IsPause)
            {
                return;
            }
            
            _pauseController.TogglePause();

            _pauseMenuVM.IsShown = _pauseController.IsPaused;
            _inputProvider.SetGameplayActive(!_pauseController.IsPaused);
        }

        private void OnDestroy()
        {
            if (_pauseController.IsPaused)
            {
                _pauseController.TogglePause();
            }
        }
    }
}