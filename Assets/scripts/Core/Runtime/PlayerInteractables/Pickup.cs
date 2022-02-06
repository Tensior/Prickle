﻿using UnityEngine;

namespace Core.PlayerInteractables
{
    public abstract class Pickup : MonoBehaviour, IInteractable<Player>
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<Player>();

            if (player == null)
            {
                return;
            }

            OnInteract(player);
        }

        public abstract void OnInteract(Player player);
    }
}