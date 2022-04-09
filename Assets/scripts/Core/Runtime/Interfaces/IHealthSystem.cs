namespace Core.Interfaces
{
    public interface IHealthSystem : IEntity
    {
        void Init(EntityType type);
        float MaxHealth { get; }
        float CurrentHealth { get; }
        void ModifyHealth(float amount); //implementations must support both positive and negative amounts
        void Kill(); //what happens when current health is <= 0
        bool IsDead { get; }
    }
}