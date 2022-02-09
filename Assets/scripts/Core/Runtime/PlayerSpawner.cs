using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
    public class PlayerSpawner : IPlayerSpawner
    {
        private readonly Player _player;
        private List<IPlayerSpawnListener> _spawnListeners = new List<IPlayerSpawnListener>();

        public PlayerSpawner(Player player)
        {
            _player = player;
        }
        
        void IPlayerSpawner.Spawn(Transform point)
        {
            _player.SpawnAt(point);

            foreach (var listener in _spawnListeners)
                listener.OnPlayerSpawn();
        }

        void IPlayerSpawner.Despawn()
        {
            throw new System.NotImplementedException();
        }

        void IPlayerSpawner.AddSpawnListener(IPlayerSpawnListener listener)
        {
            _spawnListeners.Add(listener);
        }

        void IPlayerSpawner.RemoveSpawnListener(IPlayerSpawnListener listener)
        {
            _spawnListeners.Remove(listener);
        }
    }
}