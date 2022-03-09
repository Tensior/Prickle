using Core.Interfaces;
using UnityEngine;

namespace Core.Characters
{
    [RequireComponent(typeof(CharacterRuler))]
    public abstract class Character : MonoBehaviour, ICharacter
    {
        public IHealthSystem HealthSystem;
        
        protected bool IsFrozen;

        [SerializeField] private EntityType _type;

        private CharacterRuler _characterRuler;
        private IMovementSystem _movementSystem;
        private IFireSystem _fireSystem;
        private int _currentHealth;

        IMovementSystem ICharacter.MovementSystem => _movementSystem;

        IFireSystem ICharacter.FireSystem => _fireSystem;

        IHealthSystem ICharacter.HealthSystem => HealthSystem;

        bool ICharacter.IsFrozen => IsFrozen;

        private void Start()
        {
            _characterRuler = GetComponent<CharacterRuler>();
            _movementSystem = GetComponent<IMovementSystem>();
            _fireSystem = GetComponent<IFireSystem>();
            HealthSystem = GetComponent<IHealthSystem>();
            
            _characterRuler.Init(this);
            _fireSystem?.Init(_type);
            HealthSystem?.Init(_type);
        }
    }
}