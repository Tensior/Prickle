using Core.Interactables;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
	public class PathedProjectile : DamageDealer
	{
		[SerializeField] private GameObject _destroyEffect;
		[SerializeField] private AudioClip _destroySound;
		
		private float _speed;
		private Vector3 _destination;

		public void Initialize(Vector2 destination, float speed)
		{
			_speed = speed;
			_destination = destination;
		}

		public void Update()
		{
			if (_speed == 0)
			{
				return;
			}

			var distPerFrame = Time.deltaTime * _speed;
			
			transform.position = Vector3.MoveTowards(transform.position, _destination, distPerFrame);

			var distanceSquared = (_destination - transform.position).sqrMagnitude;
			if (distanceSquared >= distPerFrame)
			{
				return;
			}

			OnStopped();
		}

		protected override void OnDamageDealt(IDamageable damageable)
		{
			base.OnDamageDealt(damageable);
			OnStopped();
		}

		protected override void OnInteractOther(Collider2D other)
		{
			base.OnInteractOther(other);
			OnStopped();
		}

		private void OnStopped()
		{
			if (_destroyEffect != null)
			{
				Instantiate(_destroyEffect, transform.position, transform.rotation);
			}

			if (_destroySound != null)
			{
				AudioSource.PlayClipAtPoint(_destroySound, transform.position);
			}


			Destroy(gameObject);
		}
	}
}

