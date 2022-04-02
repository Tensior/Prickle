using Core.Interactables;
using Core.Interfaces;
using Core.Managers;
using UnityEngine;
using Zenject;

namespace Core
{
	public class PointStar : Interactable<Player>, IPlayerSpawnListener
	{
		[SerializeField] private GameObject _effect;
		[SerializeField] private int _pointsToAdd = 1;
		[SerializeField] private AudioClip _getStarSound;
		
		private PointManager _pointManager;

		[Inject]
		public void Inject(PointManager pointManager)
		{
			_pointManager = pointManager;
		}

		public override void OnInteract(Player player)
		{
			if (_getStarSound != null)
			{
				AudioSource.PlayClipAtPoint(_getStarSound, transform.position);
			}

			if (_effect != null)
			{
				Instantiate(_effect, transform.position, transform.rotation);
			}
			
			_pointManager.AddPoints(_pointsToAdd);
			gameObject.SetActive(false);
		}

		public void OnPlayerSpawn()
		{
			gameObject.SetActive(true);
		}
	}
}


