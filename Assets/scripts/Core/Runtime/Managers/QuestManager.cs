using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Quests;
using UnityEngine;

namespace Core.Managers
{
	public class QuestManager : MonoBehaviour, IQuestManager
	{
		[SerializeField] private List<Quest> _startQuests;
		
		private readonly List<IQuest> _quests = new List<IQuest>();

		private void Awake()
		{
			throw new NotImplementedException();
		}

		private void Update()
		{
			foreach (var quest in _quests)
			{
				if (quest.CurrentStatus == QuestStatus.InProgress && quest.IsGoalReached())
				{
					quest.ApplyReward();
					quest.CurrentStatus = QuestStatus.Complete;
				}
			}
		}

		void IQuestManager.AddQuest(IQuest quest)
		{
			if (quest == null)
			{
				return;
			}
			
			quest.CurrentStatus = QuestStatus.InProgress;
			_quests.Add(quest);
		}
	}
}