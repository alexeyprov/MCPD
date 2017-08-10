using System.ComponentModel;

namespace DataTemplateCreation
{
    public class TextViewModelBase : INotifyPropertyChanged
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; RaisePropertyChanged("Text"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
