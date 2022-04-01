using UnityEngine;

namespace Core
{
	public class BackgroundParallax : MonoBehaviour
	{
		[SerializeField] private Transform[] _backgrounds;
		[SerializeField] private float _parallaxScale;
		[SerializeField] private float _parallaxReductionFactor;
		[SerializeField] private float _smoothing;

		private Vector3 _lastPosition;

		public void Start() 
		{
			_lastPosition = transform.position;
		}

		public void Update() 
		{
			var parallax = (_lastPosition.x - transform.position.x) * _parallaxScale;

			for (var i = 0; i < _backgrounds.Length; i++) 
			{
				var backgroundTargetPosition = _backgrounds[i].position.x + parallax * (i * _parallaxReductionFactor + 1);
				_backgrounds[i].position = Vector3.Lerp(
					_backgrounds[i].position,
					new Vector3(backgroundTargetPosition, _backgrounds[i].position.y, _backgrounds[i].position.z),
					_smoothing * Time.deltaTime);
			}

			_lastPosition = transform.position;
		}
	}
}
