namespace Cuboid.Modules.MediaPlayer.ViewModels
{
    /// <summary>
    /// View model for the "Video Player" view
    /// </summary>
    public class VideoPlayerViewModel : BaseMedialViewModel
    {
        public string ViewName
        {
            get
            {
                return "Video Player";
            }
        }

        protected override string OpenDialogTitle
        {
            get
            {
                return "Open video file";
            }
        }

        protected override string OpenDialogFilter
        {
            get
            {
                return "Video Files (*.mp4,*.avi,*.mov)|*.mp4;*.avi;*.mov";
            }
        }
    }
}
