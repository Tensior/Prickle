namespace Core.Interfaces
{
    public interface ICharacter
    {
        IMovementSystem MovementSystem { get; }
        IFireSystem FireSystem { get; }
        IHealthSystem HealthSystem { get; }
        bool IsFrozen { get; }
    }
}