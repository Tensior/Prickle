using Core.Interactables;
using UnityEngine;

namespace Core
{
	public class PointStar : Pickup, IPlayerSpawnListener
	{
		public GameObject Effect;
		public int PointsToAdd = 1;
		public AudioClip GetStarSound;

		public override void OnInteract(Player player)
		{
			if (GetStarSound != null)
				AudioSource.PlayClipAtPoint(GetStarSound, transform.position);

			GameManager.Instance.AddPoints(PointsToAdd);
			Instantiate(Effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerSpawn()
		{
			gameObject.SetActive(true);
		}
	}
}


