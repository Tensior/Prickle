using System;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class ControllerParameters2D 
	{
		public enum JumpBehaviour 
		{
			CanJumpOnGround,
			CanJumpAnywhere,
			CantJump,
		}

		public Vector2 MaxVelocity = new Vector2(float.MaxValue, float.MaxValue);

		[Range(0, 90)] //show in inspector, it'd be a slider between 0 and 90
		public float SlopeLimit = 30; //the angle we allowed to climb

		public float Gravity = -15f; //it's default

		public JumpBehaviour JumpRestrictions;

		public float JumpFrequency = .25f; //how often we can jump, it's a limit

		//how much force would be added to velocity.y for jumping
		public float JumpMagnitude = 12;
	}
}
