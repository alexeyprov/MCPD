using System.Windows.Controls;
using Cuboid.Modules.BasicMvvm.ViewModels;

namespace Cuboid.Modules.BasicMvvm.Views
{
    /// <summary>
    /// Interaction logic for BeatlesView.xaml
    /// </summary>
    public partial class BeatlesView : UserControl
    {
        public BeatlesView(BeatlesViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
