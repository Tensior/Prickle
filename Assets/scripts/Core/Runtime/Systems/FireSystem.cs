using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    public class FireSystem : MonoBehaviour, IFireSystem
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _damage;
        [SerializeField] private int _fireRate;
        [SerializeField] private int _fireDistance;
        [SerializeField] private Transform _weaponPivot;
        [SerializeField] private AudioClip _playerShootSound;

        private EntityType _type;
        private Animator _animator;
        private float _timeBetweenFire;
        private float _timeSinceLastFire;

        void IFireSystem.Init(EntityType type, Animator animator)
        {
            _type = type;
            _animator = animator;

            _timeBetweenFire = 1 / (float)_fireRate;
        }

        void IFireSystem.Fire()
        {
            if (_timeSinceLastFire < _timeBetweenFire)
            {
                return;
            }

            var weaponPosition = _weaponPivot.position;
            var projectile = Instantiate(_projectile, weaponPosition, _weaponPivot.rotation);
            projectile.Init(weaponPosition + _weaponPivot.right * _fireDistance, 10, _type, _damage);
            _timeSinceLastFire = 0;

            AudioSource.PlayClipAtPoint(_playerShootSound, transform.position);
            _animator.SetTrigger("Fire");
        }

        private void Update()
        {
            _timeSinceLastFire += Time.deltaTime;
        }
    }
}