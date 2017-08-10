using System.Windows;

namespace HelloWpf.UiTricks
{
    /// <summary>
    /// Interaction logic for AnimationsWindow.xaml
    /// </summary>
    public partial class AnimationsWindow : Window
    {
        public AnimationsWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //FirstAnimation.To = Width - 30.0;
            //SecondAnimation.To = Width - 30.0;
            
            //YellowEllipseAnimation.To = Canvas.ActualWidth - 30;

            //GreenEllipseAnimation.KeyFrames[0].Value = (Canvas.ActualWidth - 30) / 2;
            //GreenEllipseAnimation.KeyFrames[1].Value = Canvas.ActualWidth - 30;

            //((ScaleTransform)GreenAnimationFirstCurve.LayoutTransform).ScaleX = (Canvas.ActualWidth - 30) / 2;
            //((ScaleTransform)GreenAnimationSecondCurve.LayoutTransform).ScaleX = (Canvas.ActualWidth - 30) / 2;
        }
    }
}
