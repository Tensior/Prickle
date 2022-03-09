using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    public abstract class HealthSystem : MonoBehaviour, IHealthSystem
    {
        [SerializeField] private int _maxHealth;
        
        private EntityType _type;
        private int _currentHealth;
        private bool _isDead;

        public EntityType Type => _type;

        int IHealthSystem.MaxHealth => _maxHealth;

        int IHealthSystem.CurrentHealth => _currentHealth;

        public void Init(EntityType type)
        {
            _type = type;
            _currentHealth = _maxHealth;
        }

        void IHealthSystem.ModifyHealth(int amount)
        {
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);

            OnHealthModified(amount);

            if (_currentHealth <= 0f)
            {
                OnKilled();
            }
        }

        void IHealthSystem.Kill()
        {
            _currentHealth = 0;

            OnKilled();
        }

        bool IHealthSystem.IsDead => _currentHealth <= 0;
        
        protected abstract void OnHealthModified(int amount);

        protected abstract void OnKilled();
    }
}