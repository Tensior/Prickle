using System;
using UnityEngine;
using Zenject;

namespace Core.Interactables
{
	public class Projectile : DamageDealer, IPoolable<Vector2, float, EntityType, int, IMemoryPool>, IDisposable
	{
		[SerializeField] private GameObject _destroyEffect;
		[SerializeField] private AudioClip _destroySound;
		
		private float _speed;
		private Vector2 _destination;
		
		private IMemoryPool _pool;

		public void Update()
		{
			if (_speed == 0)
			{
				return;
			}

			var distPerFrame = Time.deltaTime * _speed;
			
			transform.position = Vector3.MoveTowards(transform.position, _destination, distPerFrame);

			var distanceSquared = (_destination - (Vector2)transform.position).sqrMagnitude;
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

			Dispose();
		}

		public void OnDespawned()
		{
			_pool = null;
			_speed = 0;
			_destination = Vector2.zero;
		}

		public void OnSpawned(
			Vector2 destination,
			float speed,
			EntityType type,
			int damage,
			IMemoryPool pool)
		{
			base.Init(type, damage);

			_speed = speed;
			_destination = destination;

			_pool = pool;
		}

		public void Dispose()
		{
			_pool.Despawn(this);
		}
		
		public class Factory : PlaceholderFactory<Vector2, float, EntityType, int, Projectile>
		{
		}
	}
}

