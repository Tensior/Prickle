using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class Checkpoint : MonoBehaviour
	{
		public void Awake()
		{
		}


		public void PlayerHitCheckpoint()
		{ 
		
		}

		private IEnumerator PlayerHitCheckpointCo(int bonus)
		{
			yield break;
		}

		public void PlayerLeftCheckpoint()
		{
		
		}

		public void RespawnEnemyData(Player player)
		{
			/*foreach (var listener in _listeners)
				listener.OnPlayerSpawn();*/
		}
	}
}

