using Core.Interactables;
using UnityEngine;

namespace Core
{
	public class Projectile : DamageDealer
	{
		[SerializeField] private GameObject _destroyEffect;
		[SerializeField] private AudioClip _destroySound;
		
		private float _speed;
		private Vector3 _destination;

		public void Init(Vector2 destination, float speed, EntityType type, int damage)
		{
			base.Init(type, damage);
			
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

		protected override void OnDamageDealt()
		{
			base.OnDamageDealt();
			OnStopped();
		}

		protected override void OnInteractOther(Collider2D other)
		{
			base.OnInteractOther(other);

			// TODO: remove it
			if (other.gameObject.layer is not (9 or 13))
			{
				OnStopped();
			}
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

