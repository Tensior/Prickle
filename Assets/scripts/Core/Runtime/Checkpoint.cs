using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class Checkpoint : MonoBehaviour
	{
		private List<IPlayerRespawnListner> _listeners;

		public void Awake()
		{
			_listeners = new List<IPlayerRespawnListner>();
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

		public void SpawnPlayer(Player player)
		{
			player.RespawnAt(transform);

			foreach (var listener in _listeners)
				listener.OnPlayerRespawnListnerInThisCheckpoint(this, player);
		}

		public void RespawnEnemyData(Player player)
		{
			foreach (var listener in _listeners)
				listener.OnPlayerRespawnListnerInThisCheckpoint(this, player);
		}

		public void AssignObjectToCheckpoint(IPlayerRespawnListner listener)
		{
			_listeners.Add(listener);
		}
	}
}

