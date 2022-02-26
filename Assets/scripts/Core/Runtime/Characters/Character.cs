﻿using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    [RequireComponent(typeof(CharacterRuler))]
    public abstract class Character : MonoBehaviour, ICharacter
    {
        protected bool IsFrozen;
        
        [SerializeField] private EntityType _type;
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        private CharacterRuler _characterRuler;
        private IMovementSystem _movementSystem;
        private IFireSystem _fireSystem;

        public EntityType Type => _type;
        
        IMovementSystem ICharacter.MovementSystem => _movementSystem;

        IFireSystem ICharacter.FireSystem => _fireSystem;

        bool ICharacter.IsFrozen => IsFrozen;

        int IDamageable.MaxHealth => _maxHealth;

        int IDamageable.CurrentHealth => _currentHealth;

        bool IDamageable.IsDead => _currentHealth <= 0;

        private void Start()
        {
            _characterRuler = GetComponent<CharacterRuler>();
            _movementSystem = GetComponent<IMovementSystem>();
            _fireSystem = GetComponent<IFireSystem>();
            
            _characterRuler.Init(this);
            _fireSystem.Init(_type);
        }

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