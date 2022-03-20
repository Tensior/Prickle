using Core.Interfaces;
using Zenject;

namespace Core.Characters
{
    public class PlayerCharacterRuler : CharacterRuler
    {
        public bool IsDead => Character.HealthSystem.IsDead;
        public bool IsFrozen => Character.IsFrozen;
        
        private IInputProvider _inputProvider;

        [Inject]
        public void Inject(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        protected override void ProcessMovement()
        {
            if (IsDead || IsFrozen)
            {
                Direction = Direction.None;
                IsJump = false;
                return;
            }
            
            if (_inputProvider.IsMoveRight)
            {
                Direction = Direction.Right;
            }
            else if (_inputProvider.IsMoveLeft)
            {
                Direction = Direction.Left;
            }
            else
            {
                Direction = Direction.None;
            }

            IsJump = _inputProvider.IsJump;
        }

        protected override void ProcessFire()
        {
            if (IsDead || IsFrozen)
            {
                IsFire = false;
                return;
            }
            
            IsFire = _inputProvider.IsFire;
        }
    }
}