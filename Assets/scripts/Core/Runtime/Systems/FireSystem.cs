using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.Systems
{
    public class FireSystem : MonoBehaviour, IFireSystem
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _projectileSpeed;
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
        private Projectile.Factory _projectileFactory;

        [Inject]
        public void Inject(DiContainer diContainer)
        {
            _projectileFactory = diContainer.TryResolveId<Projectile.Factory>(_projectile.name);
        }
        /*[Inject]
        public void Inject([Inject(Id = "player_bullet")] Projectile.Factory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }*/

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
            
            var projectile = _projectileFactory.Create(GetFireDestination(), _projectileSpeed, _type, _damage);
            projectile.transform.SetPositionAndRotation(weaponPosition, _weaponPivot.rotation);
            
            _timeSinceLastFire = 0;

            if (_shootEffect != null)
            {
                // TODO: add pools
                Instantiate(_shootEffect, weaponPosition, _weaponPivot.rotation);
            }

            if (_shootSound != null)
            {
                AudioSource.PlayClipAtPoint(_shootSound, weaponPosition);
            }
            
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