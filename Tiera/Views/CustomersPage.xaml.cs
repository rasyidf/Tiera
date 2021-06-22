using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class CustomersPage : Page
    {
        public CustomersPage(CustomersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
