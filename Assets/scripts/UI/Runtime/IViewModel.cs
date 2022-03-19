using System.ComponentModel;

namespace UI
{
    public interface IViewModel : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName = null);
    }
}