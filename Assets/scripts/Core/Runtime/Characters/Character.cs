using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityType _type;
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        public EntityType Type => _type;

        int IDamageable.MaxHealth => _maxHealth;

        int IDamageable.CurrentHealth => _currentHealth;

        void IDamageable.ModifyHealth(int amount)
        {
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);

            OnHealthModified(amount);

            if (_currentHealth <= 0f)
            {
                OnKilled();
            }
        }

        void IDamageable.Kill()
        {
            _currentHealth = 0;

            OnKilled();
        }

        protected abstract void OnHealthModified(int amount);

        protected abstract void OnKilled();
    }
}