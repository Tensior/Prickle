using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Path
{
	public class PathDefinition : MonoBehaviour 
	{
		[SerializeField] private Transform[] _points;

		public IEnumerator<Transform> GetPathEnumerator() {

			if (_points == null || _points.Length < 1)
				yield break;

			var direction = 1;
			var index = 0;

			while (true) 
			{
				yield return _points[index];

				if (_points.Length == 1)
					continue;

				if (index <= 0)
					direction = 1;
				else if (index >= _points.Length - 1)
					direction = -1;

				index = index + direction;
		
			}
		}

		public void OnDrawGizmos()
		{
			if (_points == null)
			{
				return;
			}

			var points = _points.Where(point => point != null).ToList();
			if (points.Count < 2)
			{
				return;
			}

			for (var i = 1; i < _points.Length; i++)
			{
				Gizmos.DrawLine(_points[i - 1].position, _points[i].position);
			}
		}
	}
}
