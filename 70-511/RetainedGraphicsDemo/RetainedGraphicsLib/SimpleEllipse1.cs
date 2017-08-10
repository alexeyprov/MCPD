using System;
using System.Windows;
using System.Windows.Media;

namespace RetainedGraphicsLib
{
    public class SimpleEllipse1 : FrameworkElement
    {
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill",
                typeof(Brush), typeof(SimpleEllipse1),
                new FrameworkPropertyMetadata(null, 
                FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Fill
        {
            set { SetValue(FillProperty, value); }
            get { return (Brush)GetValue(FillProperty); }
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
