using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class DashboardPage : Page
    {
        public DashboardPage(DashboardViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
