using UnityEngine;

namespace Core
{
	public class PathedProjectileSpawner : MonoBehaviour
	{
		[SerializeField] private float _maxDistance;
		public Projectile Projectile;

		public GameObject SpawnEffect;
		public float Speed;
		public float FireRate;

		public AudioSource ShootSound;
		public Animator Animator;

		private float _nextShotInSeconds;
		private Vector2 _shootTarget;

		public void Start()
		{
			_nextShotInSeconds = FireRate;
			ShootSound = GetComponent<AudioSource>();
		}

		public void Update()
		{
			_shootTarget = transform.position + transform.forward * _maxDistance;
			if ((_nextShotInSeconds -= Time.deltaTime) > 0)
				return;

			_nextShotInSeconds = FireRate;
			var projectile = Instantiate(Projectile, transform.position, transform.rotation);
			projectile.Init(_shootTarget, Speed, EntityType.Enemy, 1);

			if (SpawnEffect != null)
				Instantiate(SpawnEffect, transform.position, transform.rotation);

			if( ShootSound != null)
				ShootSound.Play();

			if (Animator != null)
				Animator.SetTrigger("Fire");
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, _shootTarget);
		}
	}
}

