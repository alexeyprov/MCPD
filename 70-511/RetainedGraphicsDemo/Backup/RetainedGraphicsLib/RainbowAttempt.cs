using System;
using System.Windows;
using System.Windows.Media;

namespace RetainedGraphicsLib
{
    public class RainbowAttempt : FrameworkElement
    {
        Color[] colors = { Colors.Red, Colors.Orange, 
                    Colors.Yellow, Colors.Green, Colors.Blue, 
                    Colors.Indigo, Colors.Violet };

        protected override void OnRender(DrawingContext dc)
        {
            SolidColorBrush brush = new SolidColorBrush();
            Rect rect = 
                new Rect(0, 0, RenderSize.Width / colors.Length, 
                               RenderSize.Height);

            foreach (Color color in colors)
            {
                brush.Color = color;
                dc.DrawRectangle(brush, null, rect);
                rect.X += RenderSize.Width / colors.Length;
            }
        }
    }
}
