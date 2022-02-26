using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    public abstract class CharacterRuler : MonoBehaviour
    {
        protected ICharacter Character;
        
        public void Init(ICharacter character)
        {
            Character = character;
        }

        protected virtual void Update()
        {
            if (Character.MovementSystem != null)
            {
                ProcessMovement(Character.MovementSystem);
            }

            if (Character.FireSystem != null)
            {
                ProcessFire(Character.FireSystem);
            }
        }

        protected abstract void ProcessMovement(IMovementSystem movementSystem);

        protected abstract void ProcessFire(IFireSystem fireSystem);
    }
}