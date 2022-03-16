using Core.Interfaces;
using UnityEngine;

namespace Core.Systems
{
    [RequireComponent(typeof(Animator), typeof(CharacterController2D))]
    public class MovementSystem : MonoBehaviour, IMovementSystem
    {
        [SerializeField] private float _maxSpeed = 8;
        [SerializeField] private float _speedAccelerationOnGround = 10f; //how quickly -player goes from moving left to the right/speed can change
        [SerializeField] private float _speedAccelerationInAir = 5f;
        [SerializeField] private AudioClip _jumpSound;
        
        private Animator _animator;
        private CharacterController2D _characterController;
        private Collider2D _collider;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController2D>();
            _collider = GetComponent<Collider2D>();
        }

        void IMovementSystem.Move(Direction horizontalDirection)
        {
            var movementFactor = _characterController.State.IsGrounded
                ? _speedAccelerationOnGround
                : _speedAccelerationInAir;

            _characterController.SetHorizontalForce(Mathf.Lerp(
                _characterController.Velocity.x,
                (int)horizontalDirection * _maxSpeed,
                Time.deltaTime * movementFactor));

            _animator.SetBool("IsGrounded", _characterController.State.IsGrounded);
            _animator.SetFloat("Speed", Mathf.Abs(_characterController.Velocity.x) / _maxSpeed);
        }

        void IMovementSystem.Jump()
        {
            if (!_characterController.CanJump)
            {
                return;
            }
            
            _characterController.Jump();
            AudioSource.PlayClipAtPoint(_jumpSound, _characterController.transform.position, 0.7f);
        }

        ControllerState2D IMovementSystem.State => _characterController.State;

        void IMovementSystem.Deactivate(Vector2 force)
        {
            _collider.enabled = false;
            _characterController.HandleCollisions = false;
            _characterController.SetForce(force);
        }

        void IMovementSystem.Activate()
        {
            _collider.enabled = true;
            _characterController.HandleCollisions = true;
        }
    }
}