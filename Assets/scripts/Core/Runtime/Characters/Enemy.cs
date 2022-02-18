using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
	public class Enemy : Character, IPlayerSpawnListener
	{
		[SerializeField] private GameObject _destroyedEffect;

		private IDamageable _thisDamageable;

		private void Awake()
		{
			_thisDamageable = this;
		}

		public void OnPlayerSpawn()
		{
			_thisDamageable.ModifyHealth(_thisDamageable.MaxHealth);
			gameObject.SetActive(true);
		}

		protected override void OnHealthModified(int amount) { }

		protected override void OnKilled()
		{
			Instantiate(_destroyedEffect, transform.position, transform.rotation);
			gameObject.SetActive(false);
		}
	}
}


