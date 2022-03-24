using Core.Interfaces;
using UnityEngine;

namespace Core
{
    public class PauseController : IPauseController
    {
        private bool _isPaused;

        void IPauseController.TogglePause()
        {
            Time.timeScale = _isPaused ? 1 : 0;

            _isPaused = !_isPaused;
        }

        bool IPauseController.IsPaused => _isPaused;
    }
}