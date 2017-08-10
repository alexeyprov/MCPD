namespace Cuboid.Modules.MediaPlayer.ViewModels
{
    /// <summary>
    /// View model for the "Audio Player" view
    /// </summary>
    public class AudioPlayerViewModel : BaseMedialViewModel
    {
        public string ViewName
        {
            get
            {
                return "Audio Player";
            }
        }

        protected override string OpenDialogTitle
        {
            get
            {
                return "Open audio file";
            }
        }

        protected override string OpenDialogFilter
        {
            get
            {
                return "Audio Files (*.mp3,*.wma,*.wav)|*.mp3;*.wma;*.wav";
            }
        }
    }
}
