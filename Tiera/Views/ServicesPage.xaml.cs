using System.Windows.Controls;

using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class ServicesPage : Page
    {
        public ServicesPage(ServicesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
