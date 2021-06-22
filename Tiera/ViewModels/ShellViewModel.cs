using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

using MahApps.Metro.Controls;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using Tiera.Contracts.Services;
using Tiera.Core.Contracts.Services;
using Tiera.Core.Helpers;
using Tiera.Helpers;
using Tiera.Properties;

namespace Tiera.ViewModels
{
    public class ShellViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IIdentityService _identityService;
        private readonly IUserDataService _userDataService;
        private HamburgerMenuItem _selectedMenuItem;
        private HamburgerMenuItem _selectedOptionsMenuItem;
        private bool _isBusy;
        private bool _isLoggedIn;
        private bool _isAuthorized;
        private RelayCommand _goBackCommand;
        private ICommand _menuItemInvokedCommand;
        private ICommand _optionsMenuItemInvokedCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        public HamburgerMenuItem SelectedOptionsMenuItem
        {
            get { return _selectedOptionsMenuItem; }
            set { SetProperty(ref _selectedOptionsMenuItem, value); }
        }
        
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellDashboardPage, Glyph = "\uE8A5", TargetPageType = typeof(DashboardViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellOrdersPage, Glyph = "\uE8A5", TargetPageType = typeof(OrdersViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellProductsPage, Glyph = "\uE8A5", TargetPageType = typeof(ProductsViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellServicesPage, Glyph = "\uE8A5", TargetPageType = typeof(ServicesViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellVetsPage, Glyph = "\uE8A5", TargetPageType = typeof(VetsViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellCustomersPage, Glyph = "\uE8A5", TargetPageType = typeof(CustomersViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellEmployeesPage, Glyph = "\uE8A5", TargetPageType = typeof(EmployeesViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellReportsPage, Glyph = "\uE8A5", TargetPageType = typeof(ReportsViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellHelpPage, Glyph = "\uE8A5", TargetPageType = typeof(HelpViewModel) },
        };

        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellSettingsPage, Glyph = "\uE713", TargetPageType = typeof(SettingsViewModel) }
        };

        public Func<HamburgerMenuItem, bool> IsPageRestricted { get; } =
            (menuItem) => Attribute.IsDefined(menuItem.TargetPageType, typeof(Restricted));

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { SetProperty(ref _isLoggedIn, value); }
        }

        public bool IsAuthorized
        {
            get { return _isAuthorized; }
            set { SetProperty(ref _isAuthorized, value); }
        }

        public RelayCommand GoBackCommand => _goBackCommand ??= new RelayCommand(OnGoBack, CanGoBack);

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ??= new RelayCommand(OnMenuItemInvoked);

        public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ??= new RelayCommand(OnOptionsMenuItemInvoked);

        public ICommand LoadedCommand => _loadedCommand ??= new RelayCommand(OnLoaded);

        public ICommand UnloadedCommand => _unloadedCommand ??= new RelayCommand(OnUnloaded);

        public ShellViewModel(INavigationService navigationService, IIdentityService identityService, IUserDataService userDataService)
        {
            _navigationService = navigationService;
            _identityService = identityService;
            _userDataService = userDataService;
            _identityService.LoggedIn += OnLoggedIn;
            _identityService.LoggedOut += OnLoggedOut;
            _userDataService.UserDataUpdated += OnUserDataUpdated;
        }

        private void OnLoaded()
        {
            _navigationService.Navigated += OnNavigated;
            IsLoggedIn = _identityService.IsLoggedIn();
            IsAuthorized = IsLoggedIn && _identityService.IsAuthorized();
            var userMenuItem = new HamburgerMenuImageItem()
            {
                Command = new RelayCommand(OnUserItemSelected, () => !IsBusy)
            };
            if (IsAuthorized)
            {
                var user = _userDataService.GetUser();
                userMenuItem.Thumbnail = user.Photo;
                userMenuItem.Label = user.Name;
            }
            else
            {
                userMenuItem.Thumbnail = ImageHelper.ImageFromAssetsFile("DefaultIcon.png");
                userMenuItem.Label = Resources.Shell_LogIn;
            }

            OptionMenuItems.Insert(0, userMenuItem);
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
            _userDataService.UserDataUpdated -= OnUserDataUpdated;
        }

        private void OnUserDataUpdated(object sender, UserViewModel user)
        {
            var userMenuItem = OptionMenuItems.OfType<HamburgerMenuImageItem>().FirstOrDefault();
            if (userMenuItem != null)
            {
                userMenuItem.Label = user.Name;
                userMenuItem.Thumbnail = user.Photo;
            }
        }

        private void OnLoggedIn(object sender, EventArgs e)
        {
            IsLoggedIn = true;
            IsAuthorized = IsLoggedIn && _identityService.IsAuthorized();
            IsBusy = false;
        }

        private void OnLoggedOut(object sender, EventArgs e)
        {
            IsLoggedIn = false;
            IsAuthorized = false;
            RemoveUserInformation();
            _navigationService.CleanNavigation();
        }

        private void RemoveUserInformation()
        {
            var userMenuItem = OptionMenuItems.OfType<HamburgerMenuImageItem>().FirstOrDefault();
            if (userMenuItem != null)
            {
                userMenuItem.Thumbnail = ImageHelper.ImageFromAssetsFile("DefaultIcon.png");
                userMenuItem.Label = Resources.Shell_LogIn;
            }
        }

        private bool CanGoBack()
            => _navigationService.CanGoBack;

        private void OnGoBack()
            => _navigationService.GoBack();

        private void OnMenuItemInvoked()
            => NavigateTo(SelectedMenuItem.TargetPageType);

        private void OnOptionsMenuItemInvoked()
            => NavigateTo(SelectedOptionsMenuItem.TargetPageType);

        private async void OnUserItemSelected()
        {
            if (!IsLoggedIn)
            {
                IsBusy = true;
                var loginResult = await _identityService.LoginAsync();
                if (loginResult != LoginResultType.Success)
                {
                    await AuthenticationHelper.ShowLoginErrorAsync(loginResult);
                    IsBusy = false;
                }
            }

            NavigateTo(typeof(SettingsViewModel));
        }

        private void NavigateTo(Type targetViewModel)
        {
            if (targetViewModel != null)
            {
                _navigationService.NavigateTo(targetViewModel.FullName);
            }
        }

        private void OnNavigated(object sender, string viewModelName)
        {
            var item = MenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
            if (item != null)
            {
                SelectedMenuItem = item;
            }
            else
            {
                SelectedOptionsMenuItem = OptionMenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
            }

            GoBackCommand.NotifyCanExecuteChanged();
        }
    }
}
