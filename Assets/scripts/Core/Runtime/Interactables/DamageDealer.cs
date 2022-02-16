using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
    public class DamageDealer : Interactable<IDamageable>, ITypedAmount
    {
        [SerializeField] private EntityType _type;
        [SerializeField] private int _damage;
        [SerializeField] private bool _isImmediateKill;

        EntityType ITypedAmount.Type => _type;

        int ITypedAmount.Amount => -_damage;

        bool ITypedAmount.IsFullAmount => _isImmediateKill;

        public override void OnInteract(IDamageable damageable)
        {
            if (_type == damageable.Type)
            {
                return;
            }

            if (_isImmediateKill)
            {
                damageable.Kill();
            }
            else
            {
                damageable.ModifyHealth(-_damage);
            }
            
            OnDamageDealt(damageable);
        }

        protected virtual void OnDamageDealt(IDamageable damageable) { }
    }
}