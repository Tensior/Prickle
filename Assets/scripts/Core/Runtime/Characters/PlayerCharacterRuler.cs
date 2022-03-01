using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    public class PlayerCharacterRuler : CharacterRuler
    {
        public bool IsDead => Character.IsDead;
        public bool IsFrozen => Character.IsFrozen;
        
        private bool _isFacingRight;
        private Direction _horizontalDirection;
        private bool _isJump;
        private bool _isFire;

        private void Awake()
        {
            _isFacingRight = transform.localScale.x > 0;
        }

        protected override void Update()
        {
            if (!IsDead)
            {
                HandleInput();
            }

            base.Update();
        }

        protected override void ProcessMovement(IMovementSystem movementSystem)
        {
            if (_horizontalDirection == Direction.Right && !_isFacingRight)
            {
                Flip();
            }

            if (_horizontalDirection == Direction.Left && _isFacingRight)
            {
                Flip();
            }

            movementSystem.Move(_horizontalDirection);

            if (_isJump)
            {
                movementSystem.Jump();
            }
        }

        protected override void ProcessFire(IFireSystem fireSystem)
        {
            if (_isFire)
            {
                fireSystem.Fire();
            }
        }

        private void HandleInput()
        {
            if (IsDead || IsFrozen)
            {
                _horizontalDirection = Direction.None;
                _isJump = false;
                _isFire = false;
                return;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                _horizontalDirection = Direction.Right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _horizontalDirection = Direction.Left;
            }
            else
            {
                _horizontalDirection = Direction.None;
            }

            _isJump = Input.GetKeyDown(KeyCode.Space);

            _isFire = Input.GetMouseButtonDown(0);
        }

        private void Flip()
        {
            transform.Rotate(Vector3.up, 180f);
            _isFacingRight = !_isFacingRight;
        }
    }
}