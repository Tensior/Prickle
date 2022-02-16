using Core.Interactables;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
	public class DamageDealerWithSound : DamageDealer
	{
		public AudioClip Sound;

		protected override void OnDamageDealt(IDamageable _)
		{
			if (Sound != null)
			{
				AudioSource.PlayClipAtPoint(Sound, transform.position);
			}
		}
	}
}

