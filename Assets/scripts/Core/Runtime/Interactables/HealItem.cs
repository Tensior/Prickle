using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
	public class HealItem : Interactable<IHealthSystem>, IPlayerSpawnListener
	{
		[SerializeField] private EntityType _type;
		[SerializeField] private GameObject _effect;
		[SerializeField] private int _healthToGive;
		[SerializeField] private bool _isFullHeal;

		public override void OnInteract(IHealthSystem healthSystem)
		{
			if (healthSystem.Type != _type)
			{
				return;
			}

			var heal = _isFullHeal ? healthSystem.MaxHealth : _healthToGive;
			
			healthSystem.ModifyHealth(heal);
			
			Instantiate(_effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerSpawn()
		{
			gameObject.SetActive(true);
		}
	}
}

