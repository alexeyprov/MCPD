using System;
using System.Windows;
using System.Windows.Media;

namespace RetainedGraphicsLib
{
    public class SimpleEllipse3 : FrameworkElement
    {
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill",
                typeof(Brush), typeof(SimpleEllipse3),
                new FrameworkPropertyMetadata(null,
                    OnFillChanged));

        public Brush Fill
        {
            set { SetValue(FillProperty, value); }
            get { return (Brush)GetValue(FillProperty); }
        }

        static void OnFillChanged(DependencyObject obj,
                        DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != args.OldValue)
                (obj as SimpleEllipse3).InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            Console.WriteLine("OnRender: " + DateTime.Now.Ticks);

            dc.DrawEllipse(Fill, null,
                new Point(RenderSize.Width / 2,
                          RenderSize.Height / 2),
                RenderSize.Width, RenderSize.Height);
        }
    }
}
