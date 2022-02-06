using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class FallingPlatform : MonoBehaviour {


		public PathDefinition Path;
		public float Speed = 1;
		public float MaxDistanceToGoal = .1f;
		public float falltime = 0.5f;

		private IEnumerator<Transform> _currentPoint;


		public void Update()
		{
			if (_currentPoint == null || _currentPoint.Current == null)
				return;

			transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

			var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;

			if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
				_currentPoint.MoveNext();
		}


		IEnumerator PlatformDown(float falltime)
		{
			while (falltime > 0)
			{
				yield return new WaitForSeconds(0.001f);
				falltime -= Time.deltaTime;
			}
			_currentPoint = Path.GetPathEnumerator();
			_currentPoint.MoveNext();

			transform.position = _currentPoint.Current.position;
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			StartCoroutine(PlatformDown(falltime));
		}
	}
}


