using Core.Managers;
using UnityEngine;
using Zenject;

namespace Core.Quests
{
    public class CollectDiamondsQuest : Quest
    {
        [SerializeField] private int _diamondCount;
        [SerializeField] private GameObject _portal;
        
        private PointManager _pointManager;

        [Inject]
        public void Inject(PointManager pointManager)
        {
            _pointManager = pointManager;
        }
        
        public override bool IsGoalReached()
        {
            return _pointManager.Points >= _diamondCount;
        }

        public override void ApplyReward()
        {
            GetComponent<AudioSource>()?.Play();
            _portal.SetActive(true);
        }
    }
}