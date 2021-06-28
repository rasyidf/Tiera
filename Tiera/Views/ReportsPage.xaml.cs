using System;

using Tiera.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class ReportsPage : Page
    {
        public ReportsViewModel ViewModel { get; } = new ReportsViewModel();

        public ReportsPage()
        {
            InitializeComponent();
        }
    }
}
