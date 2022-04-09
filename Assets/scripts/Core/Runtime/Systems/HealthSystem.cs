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

        public float MaxHealth => _maxHealth;

        public virtual float CurrentHealth { get; protected set; }

        public void Init(EntityType type)
        {
            _type = type;
            CurrentHealth = _maxHealth;
        }

        void IHealthSystem.ModifyHealth(float amount)
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

        void IHealthSystem.SetMaxHealth()
        {
            CurrentHealth = _maxHealth;
        }

        bool IHealthSystem.IsDead => CurrentHealth <= 0;
        
        protected abstract void OnHealthModified(float amount);

        protected abstract void OnKilled();
    }
}