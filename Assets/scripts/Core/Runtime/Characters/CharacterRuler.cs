using System;
using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    public abstract class CharacterRuler : MonoBehaviour
    {
        // TODO: move to MovementSystem
        [SerializeField] private Direction _initialDirection;
        [SerializeField] private bool _initialIsFacingRight;
        
        [NonSerialized] public bool IsFacingRight;

        protected ICharacter Character;
        protected Direction Direction;
        protected bool IsJump;
        protected bool IsFire;


        private void Awake()
        {
            Direction = _initialDirection;
            IsFacingRight = _initialIsFacingRight;
        }

        public void Init(ICharacter character)
        {
            Character = character;
        }

        public void Flip()
        {
            transform.Rotate(Vector3.up, 180f);
            IsFacingRight = !IsFacingRight;
        }

        protected virtual void Update()
        {
            if (Character.MovementSystem != null)
            {
                ProcessMovement();
                
                if (Direction == Direction.Right && !IsFacingRight)
                {
                    Flip();
                }

                if (Direction == Direction.Left && IsFacingRight)
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

        // Fill Direction and IsJump here
        protected abstract void ProcessMovement();

        // Fill IsFire here
        protected abstract void ProcessFire();
    }
}