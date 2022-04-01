using System.Collections;
using System.Collections.Generic;
using Core.Interactables;
using UnityEngine;

namespace Core
{
	public class FallingPlatform : Interactable<Player> 
	{
		[SerializeField] private PathDefinition _path;
		[SerializeField] private float _speed = 1;
		[SerializeField] private float _maxDistanceToGoal = 0.1f;
		[SerializeField] private float _fallTime = 0.5f;

		private IEnumerator<Transform> _pathEnumerator;

		public void Update()
		{
			if (_pathEnumerator == null || _pathEnumerator.Current == null)
				return;

			transform.position = Vector3.Lerp(transform.position, _pathEnumerator.Current.position, Time.deltaTime * _speed);

			var distanceSquared = (transform.position - _pathEnumerator.Current.position).sqrMagnitude;

			if (distanceSquared < _maxDistanceToGoal * _maxDistanceToGoal)
				_pathEnumerator.MoveNext();
		}


		IEnumerator PlatformDown(float falltime)
		{
			while (falltime > 0)
			{
				yield return new WaitForSeconds(0.001f);
				falltime -= Time.deltaTime;
			}
			_pathEnumerator = _path.GetPathEnumerator();
			_pathEnumerator.MoveNext();

			transform.position = _pathEnumerator.Current.position;
		}

		public override void OnInteract(Player subject)
		{
			StartCoroutine(PlatformDown(_fallTime));
		}
	}
}


