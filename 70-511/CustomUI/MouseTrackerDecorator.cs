using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomUI
{
    public sealed class MouseTrackerDecorator : Decorator
    {
        public static readonly DependencyProperty BackgroundColorProperty;

        static MouseTrackerDecorator()
        {
            BackgroundColorProperty = DependencyProperty.Register(
                "BackgroundColor",
                typeof(Color),
                typeof(MouseTrackerDecorator),
                new FrameworkPropertyMetadata(Colors.Yellow, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(
                CreateBrush(),
                null,
                new Rect(0, 0, ActualWidth, ActualHeight));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            InvalidateVisual();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (Child != null)
            {
                Child.Measure(constraint);
                return Child.DesiredSize;
            }

            return new Size();
        }

        private Brush CreateBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);
            }

            Point position = Mouse.GetPosition(this);

            position.X /= ActualWidth;
            position.Y /= ActualHeight;

            return new RadialGradientBrush(Colors.White, BackgroundColor)
            {
                Center = position,
                GradientOrigin = position
            };
        }
    }
}
