using System.Collections;
using UnityEngine;
using Zenject;

namespace Core
{
	public class NPCAnimator : MonoBehaviour {

		public Animator Animator;
		private BoxCollider2D _boxColliderSpirit;
		private GameObject _canvas;
		public AudioSource QuestMusic;
		
		private Player _player;

		[Inject]
		public void Initialize(Player player)
		{
			_player = player;
		}

		void Start()
		{
			_boxColliderSpirit = GetComponent<BoxCollider2D>();
			_canvas = GameObject.FindWithTag("Canvas");
			QuestMusic = GetComponent<AudioSource>();
		}


		IEnumerator FreezePlayerMovement(float time)
		{
			while (time > 0)
			{ 
				if (Animator != null)
					Animator.SetTrigger("show");
			
				_player.Freeze(true);
				yield return new WaitForSeconds(0.001f);
				time -= Time.deltaTime;
			}

			_player.Freeze(false);
			_boxColliderSpirit.enabled = false;
			_canvas.SetActive(false);
		}

		public void OnTriggerEnter2D(Collider2D other)
		{
			if (QuestMusic != null)
				QuestMusic.Play();
			StartCoroutine(FreezePlayerMovement(9));
        
		}
	}
}
