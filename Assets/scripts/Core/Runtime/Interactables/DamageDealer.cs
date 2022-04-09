using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
    public class DamageDealer : Interactable<IHealthSystem>
    {
        [SerializeField] protected AudioClip _sound;
        [SerializeField] private EntityType _type;
        [SerializeField] private float _damage;
        [SerializeField] private bool _isImmediateKill;

        protected void Init(EntityType type, float damage)
        {
            _type = type;
            _damage = damage;
        }

        public override void OnInteract(IHealthSystem healthSystem)
        {
            if (_type == healthSystem.Type)
            {
                return;
            }

            if (_isImmediateKill)
            {
                healthSystem.Kill();
            }
            else
            {
                healthSystem.ModifyHealth(-_damage);
            }

            OnDamageDealt();
        }

        protected virtual void OnDamageDealt()
        {
            if (_sound != null)
            {
                AudioSource.PlayClipAtPoint(_sound, transform.position);
            }
        }
    }
}