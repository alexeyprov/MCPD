using System.Windows.Controls;

using Cuboid.Modules.MediaPlayer.ViewModels;

namespace Cuboid.Modules.MediaPlayer.Views
{
    /// <summary>
    /// Interaction logic for VideoPlayerView.xaml
    /// </summary>
    public partial class VideoPlayerView : UserControl
    {
        public VideoPlayerView(VideoPlayerViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
