using System;

namespace CustomUI.Interop
{
    public static class InputBoxFactory
    {
        public static T CreateInputBox<T>(string header, string label, EventHandler updatedHandler = null)
            where T : IInputBox, new()
        {
            T inputBox = new T
            {
                Header = header,
                Label = label
            };

            if (updatedHandler != null)
            {
                inputBox.DataUpdated += updatedHandler;
            }

            return inputBox;
        }
    }
}
