using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    [RequireComponent(typeof(CharacterRuler))]
    public abstract class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private EntityType _type;
        
        protected bool IsFrozen;
        protected CharacterRuler CharacterRuler;

        private IFireSystem _fireSystem;
        private int _currentHealth;

        public IMovementSystem MovementSystem { get; private set; }

        IFireSystem ICharacter.FireSystem => _fireSystem;

        public IHealthSystem HealthSystem { get; private set; }

        bool ICharacter.IsFrozen => IsFrozen;

        private void Start()
        {
            CharacterRuler = GetComponent<CharacterRuler>();
            MovementSystem = GetComponent<IMovementSystem>();
            _fireSystem = GetComponent<IFireSystem>();
            HealthSystem = GetComponent<IHealthSystem>();
            
            CharacterRuler.Init(this);
            _fireSystem?.Init(_type);
            HealthSystem?.Init(_type);
        }
    }
}