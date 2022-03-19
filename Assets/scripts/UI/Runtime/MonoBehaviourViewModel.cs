using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace UI
{
    public abstract class MonoBehaviourViewModel : MonoBehaviour, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnDestroy()
        {
            foreach (var binding in GetComponentsInChildren<AbstractMemberBinding>(true))
                binding.Disconnect();
        }
    }
}
