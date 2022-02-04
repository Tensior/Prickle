using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnTrampoline : MonoBehaviour {
	public float JumpMagnitude = 20;
	public AudioClip JumpSound;
	public Animator Animator;

	public void ControllerEnter2D(CharacterController2D controller) {
		if (JumpSound != null)
			AudioSource.PlayClipAtPoint(JumpSound, transform.position, 0.6f);

		if (Animator != null)
			Animator.SetTrigger("Fire");
		
		controller.SetVerticalForce(JumpMagnitude);
	}

}
