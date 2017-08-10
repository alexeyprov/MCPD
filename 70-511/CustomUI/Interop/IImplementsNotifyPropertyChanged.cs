using System.ComponentModel;

namespace CustomUI.Interop
{
    public interface IImplementsNotifyPropertyChanged
    {
        void OnNotifyPropertyChanged(PropertyChangedEventArgs e);
    }
}
