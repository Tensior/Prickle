using UnityEngine;

namespace Core
{
	public class GiveHealth : MonoBehaviour, IPlayerRespawnListner
	{
		public GameObject Effect;
		public int HealthToGive;

		public void OnTriggerEnter2D(Collider2D other)
		{
			var player = other.GetComponent<Player>();
			if (player == null)
				return;

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

