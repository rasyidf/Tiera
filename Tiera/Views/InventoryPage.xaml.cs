using System;

using Tiera.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class InventoryPage : Page
    {
        public InventoryViewModel ViewModel { get; } = new InventoryViewModel();

        public InventoryPage()
        {
            InitializeComponent();
            Loaded += InventoryPage_Loaded;
        }

        private async void InventoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
