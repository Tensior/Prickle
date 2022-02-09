using UnityEngine;

namespace Core.Interfaces
{
    public interface IPlayerSpawner
    {
        void Spawn(Transform point);
        void Despawn();
        void AddSpawnListener(IPlayerSpawnListener listener);
        void RemoveSpawnListener(IPlayerSpawnListener listener);
    }
}