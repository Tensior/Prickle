using UnityEngine;

namespace Core.Interactables
{
	public class GiveHealth : Pickup, IPlayerSpawnListener
	{
		public GameObject Effect;
		public int HealthToGive;

		public override void OnInteract(Player player)
		{
			player.GiveHealth(HealthToGive, gameObject);
			Instantiate(Effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerSpawn()
		{
			gameObject.SetActive(true);
		}
	}
}

