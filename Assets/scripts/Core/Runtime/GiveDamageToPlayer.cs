using Core.Interactables;
using UnityEngine;

namespace Core
{
	public class GiveDamageToPlayer : DamageDealer
	{
		/*public int DamageToGive = 10;

		private Vector2
			_lastPosition,
			_velocity;

		public void LateUpdate()
		{
			_velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
			_lastPosition = transform.position;
		}

		public override void OnDamageDealt(Player player)
		{
			player.TakeDamage(DamageToGive, gameObject);
			
			// TODO: пересмотреть логику отталкивания игрока
			var controller = player.GetComponent<CharacterController2D>();
			var totalVelocity = controller.Velocity + _velocity;

			controller.SetForce(new Vector2(
				-1 * Mathf.Sign(totalVelocity.x) * Mathf.Clamp(Mathf.Abs(totalVelocity.x) * 5, 10, 20),
				-1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * 2, 0, 15)));
		}*/
	}
}

