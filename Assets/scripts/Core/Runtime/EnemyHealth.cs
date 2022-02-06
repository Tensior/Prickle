using UnityEngine;

namespace Core
{
	public class EnemyHealth : MonoBehaviour, ITakeDamage, IPlayerRespawnListner
	{
		public int EHealth { get; private set; }
		public int MaxEHealth = 120;
		public GameObject DestroyedEffect;


		public void TakeDamage(int damage, GameObject instigator)
		{
			EHealth -= damage;
			if (EHealth == 0)
			{
				Instantiate(DestroyedEffect, transform.position, transform.rotation);
				gameObject.SetActive(false);
			}

		}


		public void OnPlayerRespawnListnerInThisCheckpoint(Checkpoint checkpoint, Player player)
		{
			//Debug.Log("It's respawn time!");
			EHealth = MaxEHealth;
			gameObject.SetActive(true);

		}

	}
}


