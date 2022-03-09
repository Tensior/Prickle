using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core
{
	public class HealthBar : MonoBehaviour
	{
		private Player _player;
		
		public GameObject heartOne,
			heartTwo,
			heartThree;
		
		[Inject]
		public void Initialize(Player player)
		{
			_player = player;
		}

		private void HealthCount(bool wordOne, bool wordTwo, bool wordThree)
		{ 
			heartOne.gameObject.SetActive(wordOne);
			heartTwo.gameObject.SetActive(wordTwo);
			heartThree.gameObject.SetActive(wordThree);
		}

		public void Start()
		{
			HealthCount(true, true, true);
		}

		public void Update()
		{
			var heartNum = _player.HealthSystem.CurrentHealth;

			switch (heartNum)
			{
				case 120:
					HealthCount(true, true, true);
					break;
				case 80:
					HealthCount(true, true, false);
					break;
				case 40:
					HealthCount(true, false, false);
					break;
				case 0:
					HealthCount(false, false, false);
					break;
			}
		}
	}
}