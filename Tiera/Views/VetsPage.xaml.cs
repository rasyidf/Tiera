using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class VetsPage : Page
    {
        public VetsPage(VetsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
