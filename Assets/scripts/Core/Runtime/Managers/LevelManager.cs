using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core
{
	public class LevelManager : MonoBehaviour
	{
		private Player _player;
		
		public static LevelManager Instance { get; private set; }

		public CameraController Camera { get; private set; }
		//
		public TimeSpan RunningTime { get { return DateTime.UtcNow - _started;}}
		public int CurrentTimeBonus
		{ 
			get 
			{
				var secondDifference = (int)(BonusCutoffSeconds - RunningTime.TotalSeconds);
				return Mathf.Max(0, secondDifference) * BonusSecondMultiplier;
			}
		}
		//
		private List<Checkpoint> _checkpoints;
		public int _currentCheckpointIndex;
		//
		public DateTime _started;
		private int _savedPoints; //

		public Checkpoint DebugSpawn;
		//
		public int BonusCutoffSeconds;
		public int BonusSecondMultiplier; //
		private IPlayerSpawner _playerSpawner;

		[Inject]
		public void Initialize(Player player, IPlayerSpawner playerSpawner)
		{
			_player = player;
			_playerSpawner = playerSpawner;
		}

		public void Awake()
		{
			Instance = this;
		}

		public void Start()
		{
			_checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();
			_currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

			Camera = FindObjectOfType<CameraController>();
			//
			_started = DateTime.UtcNow;

			var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerSpawnListener>();
			foreach (var listener in listeners)
			{
				_playerSpawner.AddSpawnListener(listener);
			}

#if UNITY_EDITOR
			if (DebugSpawn != null)
			{
				_playerSpawner.Spawn(DebugSpawn.transform);
			}
			else
			{
				SpawnPlayerOnCurrentCheckpoint();
			}
#else
			SpawnPlayerOnCurrentCheckpoint();
#endif
		}

		public void Update()
		{
			var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
			if (isAtLastCheckpoint)
				return;

			var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.x - _player.transform.position.x;
			if (distanceToNextCheckpoint >= 0)
				return;

			_checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();
			_currentCheckpointIndex++;
			_checkpoints[_currentCheckpointIndex].RespawnEnemyData(_player);
			_checkpoints[_currentCheckpointIndex].PlayerHitCheckpoint();
			//
			GameManager.Instance.AddPoints(CurrentTimeBonus);
			_savedPoints = GameManager.Instance.Points;
			_started = DateTime.UtcNow;

		}

		public void KillPlayer()
		{
			StartCoroutine(KillPlayerCo());
		}

		private IEnumerator KillPlayerCo()
		{
			_playerSpawner.Despawn();
			Camera.IsFollowing = false;
			yield return new WaitForSeconds(2f);

			Camera.IsFollowing = true;

			SpawnPlayerOnCurrentCheckpoint();

			//
			_started = DateTime.UtcNow;
			GameManager.Instance.ResetPoints(_savedPoints);
		}

		private void SpawnPlayerOnCurrentCheckpoint()
		{
			if (_currentCheckpointIndex != -1)
			{
				_playerSpawner.Spawn(_checkpoints[_currentCheckpointIndex].transform);
			}
		}
	}
}
