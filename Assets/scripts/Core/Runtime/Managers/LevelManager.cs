using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.Managers
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private List<Transform> _checkpoints;

		private Player _player;
		
		public static LevelManager Instance { get; private set; }
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
		public int _currentCheckpointIndex;
		//
		public DateTime _started;
		private int _savedPoints; //

		public Transform DebugSpawn;
		//
		public int BonusCutoffSeconds;
		public int BonusSecondMultiplier; //
		private IPlayerSpawner _playerSpawner;
		private PointManager _pointManager;

		[Inject]
		public void Initialize(Player player, IPlayerSpawner playerSpawner, PointManager pointManager)
		{
			_player = player;
			_playerSpawner = playerSpawner;
			_pointManager = pointManager;
		}

		public void Awake()
		{
			Instance = this;
		}

		public void Start()
		{
			_currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

			_started = DateTime.UtcNow;

			var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerSpawnListener>();
			foreach (var listener in listeners)
			{
				_playerSpawner.AddSpawnListener(listener);
			}

#if UNITY_EDITOR
			if (DebugSpawn != null)
			{
				_playerSpawner.Spawn(DebugSpawn);
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

			var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].position.x - _player.transform.position.x;
			if (distanceToNextCheckpoint >= 0)
				return;

			_currentCheckpointIndex++;
			_pointManager.AddPoints(CurrentTimeBonus);
			_savedPoints = _pointManager.Points;
			_started = DateTime.UtcNow;

		}

		public void KillPlayer()
		{
			StartCoroutine(KillPlayerCo());
		}

		private IEnumerator KillPlayerCo()
		{
			_playerSpawner.Despawn();
			yield return new WaitForSeconds(2f);

			SpawnPlayerOnCurrentCheckpoint();

			//
			_started = DateTime.UtcNow;
			_pointManager.ResetPoints(_savedPoints);
		}

		private void SpawnPlayerOnCurrentCheckpoint()
		{
			if (_currentCheckpointIndex != -1)
			{
				_playerSpawner.Spawn(_checkpoints[_currentCheckpointIndex]);
			}
		}
	}
}
