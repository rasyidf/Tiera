using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class OrdersPage : Page
    {
        public OrdersPage(OrdersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
