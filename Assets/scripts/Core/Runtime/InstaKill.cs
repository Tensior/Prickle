using UnityEngine;

namespace Core
{
	public class InstaKill : MonoBehaviour
	{
		public AudioClip KillSound;
		public Animator Animator;

		public void OnTriggerEnter2D(Collider2D other)
		{
			var player = other.GetComponent<Player>();
			if (player == null)
				return;

			if (KillSound != null)
				AudioSource.PlayClipAtPoint(KillSound, transform.position);

			if (Animator != null)
				Animator.SetTrigger("Damage");

			LevelManager.Instance.KillPlayer();

		}
	}
}

