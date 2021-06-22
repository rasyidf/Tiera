﻿using System.Windows;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.Web.UI;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Tiera.Contracts.Services;

namespace Tiera.ViewModels
{
    public class HelpViewModel : ObservableObject
    {
        private const string DefaultUrl = "https://rasyidf.github.io/";

        private readonly ISystemService _systemService;

        private string _source;
        private bool _isLoading = true;
        private bool _isShowingFailedMessage;
        private Visibility _isLoadingVisibility = Visibility.Visible;
        private Visibility _failedMesageVisibility = Visibility.Collapsed;
        private ICommand _refreshCommand;
        private RelayCommand _browserBackCommand;
        private RelayCommand _browserForwardCommand;
        private ICommand _openInBrowserCommand;
        private WebView2 _webView;

        public string Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
                IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsShowingFailedMessage
        {
            get => _isShowingFailedMessage;
            set
            {
                SetProperty(ref _isShowingFailedMessage, value);
                FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsLoadingVisibility
        {
            get { return _isLoadingVisibility; }
            set { SetProperty(ref _isLoadingVisibility, value); }
        }

        public Visibility FailedMesageVisibility
        {
            get { return _failedMesageVisibility; }
            set { SetProperty(ref _failedMesageVisibility, value); }
        }

        public ICommand RefreshCommand => _refreshCommand ??= new RelayCommand(OnRefresh);

        public RelayCommand BrowserBackCommand => _browserBackCommand ??= new RelayCommand(() => _webView?.GoBack(), () => _webView?.CanGoBack ?? false);

        public RelayCommand BrowserForwardCommand => _browserForwardCommand ??= new RelayCommand(() => _webView?.GoForward(), () => _webView?.CanGoForward ?? false);

        public ICommand OpenInBrowserCommand => _openInBrowserCommand ??= new RelayCommand(OnOpenInBrowser);

        public HelpViewModel(ISystemService systemService)
        {
            _systemService = systemService;
            Source = DefaultUrl;
        }

        public void Initialize(WebView2 webView)
        {
            _webView = webView;
        }

        public void OnNavigationCompleted(CoreWebView2NavigationCompletedEventArgs e)
        {
            IsLoading = false;
            if (e != null && !e.IsSuccess)
            {
                // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
                IsShowingFailedMessage = true;
            }

            BrowserBackCommand.NotifyCanExecuteChanged();
            BrowserForwardCommand.NotifyCanExecuteChanged();
        }

        private void OnRefresh()
        {
            IsShowingFailedMessage = false;
            IsLoading = true;
            _webView?.Reload();
        }

        private void OnOpenInBrowser()
            => _systemService.OpenInWebBrowser(Source);
    }
}
