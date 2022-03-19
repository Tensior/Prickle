using Core.Interactables;
using UnityEngine;
using Zenject;

namespace Core
{
	public class PointStar : Interactable<Player>, IPlayerSpawnListener
	{
		public GameObject Effect;
		public int PointsToAdd = 1;
		public AudioClip GetStarSound;
		private PointManager _pointManager;

		[Inject]
		public void Inject(PointManager pointManager)
		{
			_pointManager = pointManager;
		}

		public override void OnInteract(Player player)
		{
			if (GetStarSound != null)
				AudioSource.PlayClipAtPoint(GetStarSound, transform.position);

			_pointManager.AddPoints(PointsToAdd);
			Instantiate(Effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerSpawn()
		{
			gameObject.SetActive(true);
		}
	}
}


