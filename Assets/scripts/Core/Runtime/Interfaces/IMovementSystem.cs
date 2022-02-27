namespace Core.Interfaces
{
    public interface IMovementSystem
    {
        void Move(Direction horizontalDirection);
        void Jump();
    }
}