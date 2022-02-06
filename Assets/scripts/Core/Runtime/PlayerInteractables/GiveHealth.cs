using UnityEngine;

namespace Core.PlayerInteractables
{
	public class GiveHealth : Pickup, IPlayerRespawnListner
	{
		public GameObject Effect;
		public int HealthToGive;

		public override void OnInteract(Player player)
		{
			player.GiveHealth(HealthToGive, gameObject);
			Instantiate(Effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerRespawnListnerInThisCheckpoint(Checkpoint checkpoint, Player player)
		{
			gameObject.SetActive(true);
		}
	}
}

