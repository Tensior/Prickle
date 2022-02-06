using UnityEngine;
using UnityEngine.UI;

namespace Core
{
	public class QuestGiver : MonoBehaviour 
	{ 
		public string QuestName = string.Empty;
		public Text Captions;
		public string[] CaptionText;


		void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Player")) 
				return;

			QuestSetting.QuestStatus CurrentStatus = QuestController.GetQuestStatus(QuestName);
			Captions.text = CaptionText[(int)CurrentStatus];
		}


		void OnTriggerExit2D(Collider2D other)
		{
			QuestSetting.QuestStatus CurrentStatus = QuestController.GetQuestStatus(QuestName);
			if (CurrentStatus == QuestSetting.QuestStatus.Unassigned)
				QuestController.SetQuestStatus(QuestName, QuestSetting.QuestStatus.Assigned);
		}

	}
}