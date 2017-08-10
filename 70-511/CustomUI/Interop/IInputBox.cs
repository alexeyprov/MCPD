using System;

namespace CustomUI.Interop
{
    public interface IInputBox
    {
        event EventHandler DataUpdated;

        string Header
        {
            get;
            set;
        }

        string Label
        {
            get;
            set;
        }

        string Text
        {
            get;
            set;
        }

        void Activate();
    }
}
