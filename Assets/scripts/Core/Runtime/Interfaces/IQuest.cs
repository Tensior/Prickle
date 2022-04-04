
using Core.Quests;

namespace Core.Interfaces
{
    public interface IQuest
    {
        QuestStatus CurrentStatus { get; set; }
        bool IsGoalReached();
        void ApplyReward();
    }
}