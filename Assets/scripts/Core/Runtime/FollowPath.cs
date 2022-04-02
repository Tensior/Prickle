using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class FollowPath : MonoBehaviour
	{
		private const float MAX_DISTANCE_TO_GOAL_SQR = 0.01f;
		
		[SerializeField] private PathDefinition _path;
		[SerializeField] private float _speed = 1;

		private IEnumerator<Transform> _pathEnumerator;

		public PathDefinition Path => _path;

		public void Start()
		{
			if (_path == null)
			{
				Debug.LogError("Path cannot be null", gameObject);
				return;
			}

			_pathEnumerator = _path.GetPathEnumerator();
			_pathEnumerator.MoveNext();

			if (_pathEnumerator.Current == null)
			{
				return;
			}

			transform.position = _pathEnumerator.Current.position;
		}

		public void Update()
		{
			if (_pathEnumerator == null || _pathEnumerator.Current == null)
			{
				return;
			}

			transform.position = Vector3.MoveTowards(
				transform.position,
				_pathEnumerator.Current.position,
				Time.deltaTime * _speed);

			var distanceSquared = (transform.position - _pathEnumerator.Current.position).sqrMagnitude;

			if (distanceSquared < MAX_DISTANCE_TO_GOAL_SQR)
			{
				OnBeforeMoveNext(_pathEnumerator.Current);
				_pathEnumerator.MoveNext();
			}
		}

		protected virtual void OnBeforeMoveNext(Transform prevTransform) { }
	}
}
