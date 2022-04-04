using Core.Interactables;
using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.Quests
{
	public class QuestGiver : Interactable<Player>
	{
		[SerializeField] private Quest _quest;
		
		private IQuestManager _questManager;

		[Inject]
		public void Inject(IQuestManager questManager)
		{
			_questManager = questManager;
		}

		public override void OnInteract(Player subject)
		{
			_questManager.AddQuest(_quest);
		}
	}
}