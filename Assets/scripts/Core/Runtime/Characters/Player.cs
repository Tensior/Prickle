using Core.Characters;
using UnityEngine;

namespace Core
{
	public class Player : Character 
	{
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
	}
}
