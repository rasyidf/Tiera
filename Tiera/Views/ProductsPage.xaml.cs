using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class ProductsPage : Page
    {
        public ProductsPage(ProductsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
