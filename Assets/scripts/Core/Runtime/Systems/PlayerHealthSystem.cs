using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    public class PlayerHealthSystem : HealthSystem
    {
        [SerializeField] private GameObject _ouchEffect;
        [SerializeField] private AudioClip _playerHitSound;
        [SerializeField] private AudioClip _playerHealthSound;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        protected override void OnHealthModified(int amount)
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
            LevelManager.Instance.KillPlayer();
        }
    }
}