using UnityEngine;

namespace Core.Interfaces
{
    public interface IFireSystem
    {
        void Init(EntityType type, Animator animator);
        void Fire();
    }
}