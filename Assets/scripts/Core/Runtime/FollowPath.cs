using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class FollowPath : MonoBehaviour
	{
		[SerializeField] private PathDefinition _path;
		[SerializeField] private float _speed = 1;
		[SerializeField] private float _maxDistanceToGoal = .1f;

		private IEnumerator<Transform> _pathEnumerator;

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

			if (distanceSquared < _maxDistanceToGoal * _maxDistanceToGoal)
			{
				_pathEnumerator.MoveNext();
			}
		}
	}
}
