using Core.Interfaces;
using Core.Managers;
using UI;
using UnityEngine;
using Zenject;

namespace Core.Systems
{
    public class PlayerHealthSystem : HealthSystem
    {
        [SerializeField] private GameObject _ouchEffect;
        [SerializeField] private AudioClip _playerHitSound;
        [SerializeField] private AudioClip _playerHealthSound;
        [SerializeField] private AudioClip _playerDeathSound;

        private Animator _animator;
        private LivesVM _livesVM;
        private float _currentHealth;

        public override float CurrentHealth
        {
            get => _currentHealth;
            protected set
            {
                _currentHealth = value;
                
                if (_livesVM != null)
                {
                    _livesVM.Percentage = value / MaxHealth;
                }
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        [Inject]
        public void Inject(LivesVM livesVM)
        {
            _livesVM = livesVM;
        }

        protected override void OnHealthModified(float amount)
        {
            switch (amount)
            {
                case > 0:
                    AudioSource.PlayClipAtPoint(_playerHealthSound, transform.position);
                    break;
                case < 0:
                {
                    Instantiate(_ouchEffect, transform.position, transform.rotation);

                    if (((IHealthSystem)this).CurrentHealth >= 40)
                    {
                        AudioSource.PlayClipAtPoint(_playerHitSound, transform.position);
                    }

                    _animator.SetTrigger("Damage");
                    break;
                }
            }
        }

        protected override void OnKilled()
        {
            AudioSource.PlayClipAtPoint(_playerDeathSound, transform.position, 0.5f);
            LevelManager.Instance.KillPlayer();
        }
    }
}