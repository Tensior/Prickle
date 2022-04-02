using System.Collections;
using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
	[RequireComponent(typeof(FollowPath))]
	public class FallingPlatform : Interactable<Player>, IPlayerSpawnListener 
	{
		[SerializeField] private float _timeBeforeFall = 0.5f;

		private Vector3 _defaultPosition;
		private FollowPath _followPath;

		private void Awake()
		{
			_defaultPosition = transform.position;
			_followPath = GetComponent<FollowPath>();
		}

		public override void OnInteract(Player subject)
		{
			StartCoroutine(PlatformDown(_timeBeforeFall));
		}

		private IEnumerator PlatformDown(float fallTime)
		{
			yield return new WaitForSeconds(fallTime);

			_followPath.enabled = true;

			var pathEnumerator = _followPath.Path.GetPathEnumerator();
			pathEnumerator.MoveNext();
		}

		void IPlayerSpawnListener.OnPlayerSpawn()
		{
			_followPath.enabled = false;
			transform.position = _defaultPosition;
		}
	}
}


