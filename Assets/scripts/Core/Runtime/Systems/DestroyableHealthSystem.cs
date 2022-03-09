using UnityEngine;

namespace Core.Systems
{
    public class DestroyableHealthSystem : HealthSystem
    {
        [SerializeField] private GameObject _destroyedEffect;

        protected override void OnHealthModified(int amount) { }

        protected override void OnKilled()
        {
            Instantiate(_destroyedEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}