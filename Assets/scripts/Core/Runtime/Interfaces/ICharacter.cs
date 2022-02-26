namespace Core.Interfaces
{
    public interface ICharacter : IDamageable
    {
        IMovementSystem MovementSystem { get; }
        IFireSystem FireSystem { get; }
        bool IsFrozen { get; }
    }
}