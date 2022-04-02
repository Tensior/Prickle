using UnityEngine;

namespace Core.Interfaces
{
    public interface IMovementSystem
    {
        void Move(Direction horizontalDirection);
        void Jump();
        void Deactivate(Vector2 force);
        void Activate();
        ControllerState2D State { get; }
        CharacterController2D Controller2D { get; }
    }
}