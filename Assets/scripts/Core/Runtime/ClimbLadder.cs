using UnityEngine;

namespace Core
{
	public class ClimbLadder : MonoBehaviour
	{
		public Player player;
		public float Speed = 5f;

		void OnTriggerStay2D(Collider2D col)
		{
			if (Input.GetKey(KeyCode.W))
				player.transform.Translate(0, Speed * Time.deltaTime, 0);
			else if (Input.GetKey(KeyCode.S))
				player.transform.Translate(0, -Speed * Time.deltaTime, 0);
		}
			
	}
}
		


