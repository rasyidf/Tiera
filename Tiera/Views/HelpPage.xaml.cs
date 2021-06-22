using System.Windows.Controls;

using Microsoft.Web.WebView2.Core;
using Tiera.ViewModels;

namespace Tiera.Views
{
    public partial class HelpPage : Page
    {
        private readonly HelpViewModel _viewModel;

        public HelpPage(HelpViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            _viewModel.Initialize(webView);
        }

        private void OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
            => _viewModel.OnNavigationCompleted(e);
    }
}
