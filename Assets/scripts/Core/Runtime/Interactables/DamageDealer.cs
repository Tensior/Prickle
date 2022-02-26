using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
    public class DamageDealer : Interactable<IDamageable>, ITypedAmount
    {
        [SerializeField] private bool _isImmediateKill;

        EntityType ITypedAmount.Type => _type;
        int ITypedAmount.Amount => -_damage;
        bool ITypedAmount.IsFullAmount => _isImmediateKill;

        private EntityType _type;
        private int _damage;

        public void Init(EntityType type, int damage)
        {
            _type = type;
            _damage = damage;
        }

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