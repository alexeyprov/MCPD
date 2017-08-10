using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Cuboid.Modules.MediaPlayer.ViewModels;

namespace Cuboid.Modules.MediaPlayer.Views
{
    /// <summary>
    /// Interaction logic for AudioPlayerView.xaml
    /// </summary>
    public partial class AudioPlayerView : UserControl
    {
        private bool _isUpdatedByBinding;

        public AudioPlayerView(AudioPlayerViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string source = _isUpdatedByBinding ? "binding" : "user";
            Debug.WriteLine("Slider_ValueChanged from {0} to {1} by {2}", e.OldValue, e.NewValue, source);

            e.Handled = _isUpdatedByBinding;

            _isUpdatedByBinding = false;
        }

        private void Behavior_Refreshing(object sender, EventArgs e)
        {
            Debug.WriteLine("Behavior_Refreshing");
            _isUpdatedByBinding = true;
        }
    }
}
