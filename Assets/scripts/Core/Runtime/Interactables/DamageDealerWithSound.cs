using Core.Interactables;
using UnityEngine;

namespace Core
{
	public class DamageDealerWithSound : DamageDealer
	{
		public AudioClip Sound;

		protected override void OnDamageDealt()
		{
			if (Sound != null)
			{
				AudioSource.PlayClipAtPoint(Sound, transform.position);
			}
		}
	}
}

