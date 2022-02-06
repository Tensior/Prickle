using UnityEngine;

namespace Core
{
	public class PointStar : MonoBehaviour, IPlayerRespawnListner
	{
		public GameObject Effect;
		public int PointsToAdd = 1;
		public AudioClip GetStarSound;


		public void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<Player>() == null)
				return;

			if (GetStarSound != null)
				AudioSource.PlayClipAtPoint(GetStarSound, transform.position);

			GameManager.Instance.AddPoints(PointsToAdd);
			Instantiate(Effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerRespawnListnerInThisCheckpoint(Checkpoint checkpoint, Player player)
		{
			gameObject.SetActive(true);
		}
	}
}


