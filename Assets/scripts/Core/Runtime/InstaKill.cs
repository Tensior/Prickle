using Core.PlayerInteractables;
using UnityEngine;

namespace Core
{
	public class InstaKill : Pickup
	{
		public AudioClip KillSound;

		public override void OnInteract(Player _)
		{
			if (KillSound != null)
				AudioSource.PlayClipAtPoint(KillSound, transform.position);

			LevelManager.Instance.KillPlayer();
		}
	}
}

