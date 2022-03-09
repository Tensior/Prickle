﻿namespace Core.Interfaces
{
    public interface IHealthSystem : IEntity
    {
        void Init(EntityType type);
        int MaxHealth { get; }
        int CurrentHealth { get; }
        void ModifyHealth(int amount); //implementations must support both positive and negative amounts
        void Kill(); //what happens when current health is <= 0
        bool IsDead { get; }
    }
}