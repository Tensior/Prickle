using UnityEngine;

namespace Core.Interfaces
{
    public interface IFireSystem
    {
        void Init(EntityType type);
        void Fire();
        int FireDistance { get; }
        Vector2 WeaponPivot { get; }
    }
}