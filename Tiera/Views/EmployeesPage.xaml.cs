using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class EmployeesPage : Page
    {
        public EmployeesPage(EmployeesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
