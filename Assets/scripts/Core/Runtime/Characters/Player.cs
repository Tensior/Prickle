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
			MovementSystem.Deactivate(new Vector2(0, 10));
		}

		public void SpawnAt(Transform point)
		{
			MovementSystem.Activate();
			HealthSystem.ModifyHealth(HealthSystem.MaxHealth);
			if (!CharacterRuler.IsFacingRight)
			{
				CharacterRuler.Flip();
			}

			transform.position = point.position;
		}
	}
}
