﻿using Core.Characters;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
	public class Player : Character 
	{
		public GameObject OuchEffect;
		public AudioClip PlayerHitSound;
		public AudioClip PlayerHealthSound;

		public void Freeze(bool freeze)
		{
			IsFrozen = freeze;
		}

		public void OnDied()
		{
			/*_controller.HandleCollisions = false;
			GetComponent<Collider2D>().enabled = false;
			AudioSource.PlayClipAtPoint(DeathSound, transform.position, 0.5f);
			_controller.SetForce(new Vector2(0, 10));*/
		}

		public void SpawnAt(Transform point)
		{
			/*if (!_isFacingRight)
				Flip();

			IsDead = false;
			GetComponent<Collider2D>().enabled = true;
			_controller.HandleCollisions = true;
			Health = ((IDamageable)this).MaxHealth;*/

			transform.position = point.position;
		}

		protected override void OnHealthModified(int amount)
		{
			switch (amount)
			{
				case > 0:
					AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position);
					break;
				case < 0:
				{
					Instantiate(OuchEffect, transform.position, transform.rotation);

					if (((IDamageable)this).CurrentHealth >= 40)
					{
						AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
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
