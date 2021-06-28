using System;

using Tiera.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class ServicesPage : Page
    {
        public ServicesViewModel ViewModel { get; } = new ServicesViewModel();

        public ServicesPage()
        {
            InitializeComponent();
            Loaded += ServicesPage_Loaded;
        }

        private async void ServicesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
