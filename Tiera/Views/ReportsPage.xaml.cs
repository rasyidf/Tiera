using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class ReportsPage : Page
    {
        public ReportsPage(ReportsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
