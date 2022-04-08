using Core.Interactables;
using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.Quests
{
	public class QuestGiver : Interactable<Player>
	{
		[SerializeField] private Quest _quest;
		[SerializeField] private Animator _dialogAnimator;
		[SerializeField] private AudioSource _questAudio;
		
		private IQuestManager _questManager;
		private static readonly int Show = Animator.StringToHash("show");

		[Inject]
		public void Inject(IQuestManager questManager)
		{
			_questManager = questManager;
		}

		public override void OnInteract(Player player)
		{
			_questManager.AddQuest(_quest);
			_dialogAnimator.SetTrigger(Show);
			
			if (_questAudio != null)
			{
				_questAudio.Play();
			}

			enabled = false;
		}
	}
}