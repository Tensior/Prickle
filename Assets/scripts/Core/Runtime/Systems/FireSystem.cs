using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    public class FireSystem : MonoBehaviour, IFireSystem
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private int _fireDistance;
        [SerializeField] private Transform _weaponPivot;
        [SerializeField] private AudioClip _shootSound;
        [SerializeField] private GameObject _shootEffect;

        private EntityType _type;
        private Animator _animator;
        private float _timeBetweenFire;
        private float _timeSinceLastFire;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _timeBetweenFire = 1 / _fireRate;
        }

        void IFireSystem.Init(EntityType type)
        {
            _type = type;
        }
        
        void IFireSystem.Fire()
        {
            if (_timeSinceLastFire < _timeBetweenFire)
            {
                return;
            }

            var weaponPosition = _weaponPivot.position;
            
            var projectile = Instantiate(_projectile, weaponPosition, _weaponPivot.rotation);
            projectile.Init(GetFireDestination(), 10, _type, _damage);
            
            _timeSinceLastFire = 0;

            if (_shootEffect != null)
                Instantiate(_shootEffect, weaponPosition, _weaponPivot.rotation);
            AudioSource.PlayClipAtPoint(_shootSound, weaponPosition);
            _animator.SetTrigger("Fire");
        }

        int IFireSystem.FireDistance => _fireDistance;
        
        Vector2 IFireSystem.WeaponPivot => _weaponPivot.position;

        private void Update()
        {
            _timeSinceLastFire += Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_weaponPivot.position, GetFireDestination());
        }

        private Vector2 GetFireDestination()
        {
            return _weaponPivot.position + _weaponPivot.right * _fireDistance;
        }
    }
}