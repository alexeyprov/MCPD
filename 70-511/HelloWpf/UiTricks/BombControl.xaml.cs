using System.Windows.Controls;

namespace HelloWpf.UiTricks
{
    /// <summary>
    /// Interaction logic for BombControl.xaml
    /// </summary>
    public partial class BombControl : UserControl
    {
        public BombControl()
        {
            InitializeComponent();
        }

        public bool IsFalling
        {
            get;
            set;
        }
    }
}
