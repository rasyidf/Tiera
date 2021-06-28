using System;

using Tiera.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tiera.Views
{
    public sealed partial class VetsPage : Page
    {
        public VetsViewModel ViewModel { get; } = new VetsViewModel();

        public VetsPage()
        {
            InitializeComponent();
            Loaded += VetsPage_Loaded;
        }

        private async void VetsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState);
        }
    }
}
