using UnityEngine;

namespace Core.Characters
{
    public class PlayerCharacterRuler : CharacterRuler
    {
        public bool IsDead => Character.HealthSystem.IsDead;
        public bool IsFrozen => Character.IsFrozen;

        protected override void ProcessMovement()
        {
            if (IsDead || IsFrozen)
            {
                Direction = Direction.None;
                IsJump = false;
                return;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                Direction = Direction.Right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Direction = Direction.Left;
            }
            else
            {
                Direction = Direction.None;
            }

            IsJump = Input.GetKeyDown(KeyCode.Space);
        }

        protected override void ProcessFire()
        {
            if (IsDead || IsFrozen)
            {
                IsFire = false;
                return;
            }
            
            IsFire = Input.GetMouseButtonDown(0);
        }
    }
}