using System;

using Tiera.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardViewModel ViewModel { get; } = new DashboardViewModel();

        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}
