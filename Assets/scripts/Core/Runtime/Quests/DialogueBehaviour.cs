using UnityEngine;
using Zenject;

namespace Core.Quests
{
    public class DialogueBehaviour : StateMachineBehaviour
    {
        private Player _player;

        [Inject]
        public void Inject(Player player)
        {
            _player = player;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Freeze player");
            _player.Freeze(true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Unfreeze player");
            _player.Freeze(false);
        }
    }
}
