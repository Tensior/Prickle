using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
	public class HealItem : Interactable<IDamageable>, ITypedAmount, IPlayerSpawnListener
	{
		[SerializeField] private EntityType _type;
		[SerializeField] private GameObject _effect;
		[SerializeField] private int _healthToGive;
		[SerializeField] private bool _isFullHeal;

		EntityType ITypedAmount.Type => _type;

		int ITypedAmount.Amount => _healthToGive;

		bool ITypedAmount.IsFullAmount => _isFullHeal;

		public override void OnInteract(IDamageable damageable)
		{
			var heal = _isFullHeal ? damageable.MaxHealth : _healthToGive;
			
			damageable.ModifyHealth(heal);
			
			Instantiate(_effect, transform.position, transform.rotation);

			gameObject.SetActive(false);
		}

		public void OnPlayerSpawn()
		{
			gameObject.SetActive(true);
		}
	}
}

