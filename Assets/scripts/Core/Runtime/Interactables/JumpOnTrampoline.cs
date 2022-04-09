using UnityEngine;

namespace Core.Interactables
{
	public class JumpOnTrampoline : Interactable<Player> 
	{
		private static readonly int Fire = Animator.StringToHash("Fire");
		
		[SerializeField] private float _jumpMagnitude = 20;
		[SerializeField] private AudioClip _jumpSound;
		[SerializeField] private Animator _animator;

		public override void OnInteract(Player player)
		{
			if (_jumpSound != null)
			{
				AudioSource.PlayClipAtPoint(_jumpSound, transform.position, 0.6f);
			}

			if (_animator != null)
			{
				_animator.SetTrigger(Fire);
			}
		
			player.MovementSystem.Controller2D.SetVerticalForce(_jumpMagnitude);
		}
	}
}
