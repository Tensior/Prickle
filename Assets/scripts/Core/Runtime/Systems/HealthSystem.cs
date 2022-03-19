using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    public abstract class HealthSystem : MonoBehaviour, IHealthSystem
    {
        [SerializeField] private int _maxHealth;
        
        private EntityType _type;
        private bool _isDead;

        public EntityType Type => _type;

        public int MaxHealth => _maxHealth;

        public virtual int CurrentHealth { get; protected set; }

        public void Init(EntityType type)
        {
            _type = type;
            CurrentHealth = _maxHealth;
        }

        void IHealthSystem.ModifyHealth(int amount)
        {
            CurrentHealth = Mathf.Min(_maxHealth, CurrentHealth + amount);

            OnHealthModified(amount);

            if (CurrentHealth <= 0f)
            {
                OnKilled();
            }
        }

        void IHealthSystem.Kill()
        {
            CurrentHealth = 0;

            OnKilled();
        }

        bool IHealthSystem.IsDead => CurrentHealth <= 0;
        
        protected abstract void OnHealthModified(int amount);

        protected abstract void OnKilled();
    }
}