using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class ImageButton : Button
{
    public ImageSource Source
    {
        get
        {
            return (ImageSource) base.GetValue(SourceProperty);
        }
        set
        {
            base.SetValue(SourceProperty, value);
        }
    }

    public static readonly DependencyProperty SourceProperty = 
        DependencyProperty.Register("Source", typeof(ImageSource),
            typeof(ImageButton));
}