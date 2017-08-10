using System;
using System.Windows;
using System.Windows.Media;

namespace RetainedGraphicsLib
{
    public class SimpleEllipse2 : FrameworkElement
    {
        SolidColorBrush brush = new SolidColorBrush();

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill",
                typeof(Color), typeof(SimpleEllipse2),
                new FrameworkPropertyMetadata(Colors.Transparent,
                    OnFillChanged));

        public Color Fill
        {
            set { SetValue(FillProperty, value); }
            get { return (Color)GetValue(FillProperty); }
        }

        static void OnFillChanged(DependencyObject obj,
                        DependencyPropertyChangedEventArgs args)
        {
            (obj as SimpleEllipse2).brush.Color = 
                                        (Color)args.NewValue;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Console.WriteLine("OnRender: " + DateTime.Now.Ticks);

            dc.DrawEllipse(brush, null,
                new Point(RenderSize.Width / 2,
                          RenderSize.Height / 2),
                RenderSize.Width, RenderSize.Height);
        }
    }
}
