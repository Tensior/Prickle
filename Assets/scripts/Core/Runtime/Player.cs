using Core.Characters;
using UnityEngine;

namespace Core
{
	public class Player : Character {
		private bool _isFacingRight;
		private CharacterController2D _controller; //for manipulation with charactercontroller2d
		private float _normalizedHorizontalSpeed; // -1 or 1 depending to the player's moving to the left\right or Idle

		public float MaxSpeed = 8;
		public float SpeedAccelerationOnGround = 10f; //how quickly -player goes from moving left to the right/speed can change
		public float SpeedAccelerationInAir = 5f;

		public int MaxHealth = 120;
		public GameObject OuchEffect;
		public Projectile Projectile;
		public float FireRate;
		public Transform ProjectileFireLocation;

		public AudioClip PlayerHitSound;
		public AudioClip PlayerShootSound;
		public AudioClip PlayerHealthSound;
		public AudioClip DeathSound;
		public AudioClip JumpSound;
		public Animator Animator;

		public int Health { get; private set;}

		public bool IsDead { get; private set;}
		public bool IsFreeze { get; private set;}

		private float _canFireIn;

		public void Awake() {
			_controller = GetComponent<CharacterController2D>();
			_isFacingRight = transform.localScale.x > 0;
		}

		public void Update() {
			_canFireIn -= Time.deltaTime;
			if(!IsDead)
				HandleInput(); //change horizontal speed to -1/0/1 depending on keys

			var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

			if (IsDead || IsFreeze)
				_controller.SetHorizontalForce(0);
			else
				_controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

			Animator.SetBool("IsGrounded", _controller.State.IsGrounded);
			Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
		}

		public void Freeze(bool freeze)
		{
			IsFreeze = freeze;
		}

		public void OnDied()
		{
			_controller.HandleCollisions = false;
			GetComponent<Collider2D>().enabled = false;

			IsDead = true;
			Health = 0;
			AudioSource.PlayClipAtPoint(DeathSound, transform.position, 0.5f);
			_controller.SetForce(new Vector2(0, 10));
		}

		public void SpawnAt(Transform point)
		{
			if (!_isFacingRight)
				Flip();

			IsDead = false;
			GetComponent<Collider2D>().enabled = true;
			_controller.HandleCollisions = true;
			Health = MaxHealth;

			transform.position = point.position;
		}

		private void HandleInput() {

			if (Input.GetKey(KeyCode.D))
			{
				_normalizedHorizontalSpeed = 1;
				if (!_isFacingRight)
					Flip();

			}
			else if (Input.GetKey(KeyCode.A))
			{
				_normalizedHorizontalSpeed = -1;
				if (_isFacingRight)
					Flip();
			}
			else {
				_normalizedHorizontalSpeed = 0;
			}

			if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space)) 
			{ 
				_controller.Jump();
				AudioSource.PlayClipAtPoint(JumpSound, transform.position, 0.7f);
			}
			
			if (Input.GetMouseButtonDown(0))
				FireProjectile();
	
		}

		private void FireProjectile()
		{
			if (_canFireIn > 0)
				return;

			var direction = _isFacingRight ? Vector2.right : -Vector2.right;
			var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
			projectile.Initialize(gameObject, direction, _controller.Velocity);

			_canFireIn = FireRate;
			AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);
			Animator.SetTrigger("Fire");
		}

		private void Flip() {
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			_isFacingRight = !_isFacingRight; //transform.localScale.x > 0; 3buzz ver
		}

		protected override void OnHealthModified(int amount)
		{
			switch (amount)
			{
				case > 0:
					AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position);
					break;
				case < 0:
				{
					Instantiate(OuchEffect, transform.position, transform.rotation);

					if (Health >= 40)
					{
						AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
					}

					Animator.SetTrigger("Damage");
					break;
				}
			}
		}

		protected override void OnKilled()
		{
			LevelManager.Instance.KillPlayer();
		}
	}
}
