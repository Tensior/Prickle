using UnityEngine;

namespace Core
{
	public class SimpleEnemyAI : MonoBehaviour, IPlayerSpawnListener
	{
		public float Speed;
		public float FireRate = 1;
		public PathedProjectile Projectile;
		public GameObject DestroyedEffect;
		public AudioClip ShootSound;


		private CharacterController2D _controller;
		private Vector2 _direction;
		private Vector2 _startPosition;
		private float _canFireIn;
		private float _enemyScaleX;
		private float _enemyScaleY;

		public int EHealth { get; private set;}
		public int MaxEHealth = 120;

		public void Start()
		{
			_controller = GetComponent<CharacterController2D>();
			_direction = new Vector2(-1, 0);
			_startPosition = transform.position;
			_enemyScaleX = transform.localScale.x;
			_enemyScaleY = transform.localScale.y;

		}

		public void Update()
		{
			_controller.SetHorizontalForce(_direction.x * Speed);

			if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
			{
				_direction = -_direction;
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}

			if ((_canFireIn -= Time.deltaTime) > 0)
				return;

			var raycast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));
			if (!raycast)
				return;

			var projectile = Instantiate(Projectile, transform.position, transform.rotation);
			projectile.Initialize(_direction, 10);
			_canFireIn = FireRate;

			if (ShootSound != null)
				AudioSource.PlayClipAtPoint(ShootSound, transform.position);
		}

		public void OnPlayerSpawn() 
		{
			_direction = new Vector2(-1, 0);
			transform.position = _startPosition;
			transform.localScale = new Vector3(_enemyScaleX, _enemyScaleY, 1);
			EHealth = MaxEHealth;
			gameObject.SetActive(true);

		}

	}
}

