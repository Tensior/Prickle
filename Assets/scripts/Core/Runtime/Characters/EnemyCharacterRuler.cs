using System;
using UnityEngine;

namespace Core.Characters
{
    public class EnemyCharacterRuler : CharacterRuler
    {
        [SerializeField] private bool _fireWhenPlayerVisible;
        
        protected override void ProcessMovement()
        {
            IsJump = false;
            
            switch(Direction)
            {
                case Direction.None:
                    break;
                case Direction.Right:
                    if (Character.MovementSystem.State.IsCollidingRight)
                    {
                        Direction = Direction.Left;
                    }
                    break;
                case Direction.Left:
                    if (Character.MovementSystem.State.IsCollidingLeft)
                    {
                        Direction = Direction.Right;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void ProcessFire()
        {
            if (_fireWhenPlayerVisible)
            {
                IsFire = Physics2D.Raycast(
                    transform.position,
                    Direction.ToVector2(),
                    Character.FireSystem.FireDistance,
                    1 << LayerMask.NameToLayer("Player"));
            }
            else
            {
                IsFire = true;
            }
        }
    }
}