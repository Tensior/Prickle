using Core.Interfaces;

namespace Core.Providers
{
    public class InputProvider : IInputProvider
    {
        private readonly GameplayControls _controls;

        public InputProvider(GameplayControls controls)
        {
            _controls = controls;
            _controls.Enable();
        }

        bool IInputProvider.IsMoveLeft => _controls.Gameplay.MoveLeft.IsPressed();

        bool IInputProvider.IsMoveRight => _controls.Gameplay.MoveRight.IsPressed();

        bool IInputProvider.IsFire => _controls.Gameplay.Fire.IsPressed();

        bool IInputProvider.IsJump => _controls.Gameplay.Jump.IsPressed();
    }
}
