using UnityEngine;

namespace Core.Interfaces
{
    public interface IMovementSystem
    {
        void Move(Direction horizontalDirection);
        void Jump();
        ControllerState2D State { get; }
        void Deactivate(Vector2 force);
        void Activate();
    }
}