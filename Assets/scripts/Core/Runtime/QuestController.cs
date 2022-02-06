using System;
using System.Collections;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class QuestSetting
	{ //QuestManager -> QuestController
		public enum QuestStatus
		{
			Unassigned = 0,
			Assigned = 1,
			Complete = 2
		}

		public QuestStatus CurrentStatus = QuestStatus.Unassigned;
		public string QuestName;
	}

	public class QuestController : MonoBehaviour
	{

		public QuestSetting[] Quests;
		private static QuestController SingletonInstance = null;
		public static QuestController ThisInstance
		{
			get
			{
				if (SingletonInstance == null)
				{
					GameObject QuestObject = new GameObject("Default");
					SingletonInstance = QuestObject.AddComponent<QuestController>();
				}
				return SingletonInstance;
			}
		}


		public void Awake()
		{ 
			SingletonInstance = this;
		}


		public static QuestSetting.QuestStatus GetQuestStatus(string QuestName)
		{
			foreach (QuestSetting Q in ThisInstance.Quests)
			{
				if (Q.QuestName.Equals(QuestName))
					return Q.CurrentStatus;
			}

			return QuestSetting.QuestStatus.Unassigned;
		}


		public static void SetQuestStatus(string QuestName, QuestSetting.QuestStatus NewStatus)
		{
			foreach (QuestSetting Q in ThisInstance.Quests)
			{
				if (Q.QuestName.Equals(QuestName))
				{
					Q.CurrentStatus = NewStatus;
					return;
				}
			}
		}

		public static void Reset()
		{
			if (ThisInstance == null)
				return;

			foreach (QuestSetting Q in ThisInstance.Quests)
				Q.CurrentStatus = QuestSetting.QuestStatus.Unassigned;

		}

	}
}