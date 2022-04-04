using Core.Interfaces;
using UnityEngine;

namespace Core.Quests
{
    public abstract class Quest : MonoBehaviour, IQuest
    {
        QuestStatus IQuest.CurrentStatus { get; set; }
        public abstract bool IsGoalReached();
        public abstract void ApplyReward();
    }
}