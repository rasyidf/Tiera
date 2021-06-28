using System;

using Tiera.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class CustomerPage : Page
    {
        public CustomerViewModel ViewModel { get; } = new CustomerViewModel();

        public CustomerPage()
        {
            InitializeComponent();
            Loaded += CustomerPage_Loaded;
        }

        private async void CustomerPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
