using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    public abstract class CharacterRuler : MonoBehaviour
    {
        [SerializeField] private Direction _initialDirection;
        [SerializeField] private bool _initialIsFacingRight;

        protected ICharacter Character;
        protected Direction Direction;
        protected bool IsJump;
        protected bool IsFire;
        private bool _isFacingRight;

        private void Awake()
        {
            Direction = _initialDirection;
            _isFacingRight = _initialIsFacingRight;
        }

        public void Init(ICharacter character)
        {
            Character = character;
        }

        protected virtual void Update()
        {
            if (Character.MovementSystem != null)
            {
                ProcessMovement();
                
                if (Direction == Direction.Right && !_isFacingRight)
                {
                    Flip();
                }

                if (Direction == Direction.Left && _isFacingRight)
                {
                    Flip();
                }

                Character.MovementSystem.Move(Direction);

                if (IsJump)
                {
                    Character.MovementSystem.Jump();
                }
            }

            if (Character.FireSystem != null)
            {
                ProcessFire();
                
                if (IsFire)
                {
                    Character.FireSystem.Fire();
                }
            }
        }

        // Fill _horizontalDirection and _isJump here
        protected abstract void ProcessMovement();

        // Fill _isFire here
        protected abstract void ProcessFire();
        
        private void Flip()
        {
            transform.Rotate(Vector3.up, 180f);
            _isFacingRight = !_isFacingRight;
        }
    }
}