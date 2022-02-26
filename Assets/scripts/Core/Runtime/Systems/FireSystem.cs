using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    public class FireSystem : MonoBehaviour, IFireSystem
    {
        [SerializeField] private PathedProjectile _projectile;
        [SerializeField] private int _fireRate;
        [SerializeField] private Transform _weaponPivot;
        [SerializeField] private AudioClip _playerShootSound;
        [SerializeField] private Animator _animator;
        
        private float _canFireIn;
        private EntityType _type;

        void IFireSystem.Init(EntityType type)
        {
            _type = type;
        }

        void IFireSystem.Fire()
        {
            if (_canFireIn > 0)
                return;

            var direction = /*_isFacingRight ? Vector2.right : -*/Vector2.right;
            var projectile = Instantiate(_projectile, _weaponPivot.position, _weaponPivot.rotation);
            projectile.Initialize(direction, 10);

            _canFireIn = _fireRate;
            AudioSource.PlayClipAtPoint(_playerShootSound, transform.position);
            _animator.SetTrigger("Fire");
        }
    }
}