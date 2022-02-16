using Core.Interfaces;
using UnityEngine;

namespace Core.Interactables
{
    public abstract class Interactable<T> : MonoBehaviour, IInteractable<T> where T : class
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            var subject = other.GetComponent<T>();

            if (subject == null)
            {
                return;
            }

            OnInteract(subject);
        }

        public abstract void OnInteract(T subject);
    }
}