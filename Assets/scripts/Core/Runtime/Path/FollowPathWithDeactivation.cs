using UnityEngine;

namespace Core.Path
{
    public class FollowPathWithDeactivation : FollowPath
    {
        [SerializeField] private Transform _deactivationPoint;
        [SerializeField] private Transform _activationPoint;
        [SerializeField] private GameObject _gameObject;

        protected override void OnBeforeMoveNext(Transform previousTransform)
        {
            base.OnBeforeMoveNext(previousTransform);
            
            if (previousTransform == _deactivationPoint)
            {
                _gameObject.SetActive(false);
            }
            else if (previousTransform == _activationPoint)
            {
                _gameObject.SetActive(true);
            }
        }
    }
}