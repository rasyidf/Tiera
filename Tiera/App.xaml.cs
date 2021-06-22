using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Uwp.Notifications;

using Tiera.Activation;
using Tiera.Contracts.Activation;
using Tiera.Contracts.Services;
using Tiera.Contracts.Views;
using Tiera.Core.Contracts.Services;
using Tiera.Core.Services;
using Tiera.Models;
using Tiera.Services;
using Tiera.ViewModels;
using Tiera.Views;

namespace Tiera
{
    // For more inforation about application lifecyle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

    // WPF UI elements use language en-US by default.
    // If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
    // Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
    public partial class App : Application
    {
        private IHost _host;

        public T GetService<T>()
            where T : class
            => _host.Services.GetService(typeof(T)) as T;

        public App()
        {
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            ToastNotificationManagerCompat.OnActivated += (toastArgs) =>
            {
                Current.Dispatcher.Invoke(async () =>
                {
                    var config = GetService<IConfiguration>();
                    config[ToastNotificationActivationHandler.ActivationArguments] = toastArgs.Argument;
                    await _host.StartAsync();
                });
            };

            // TODO: Register arguments you want to use on App initialization
            var activationArgs = new Dictionary<string, string>
            {
                { ToastNotificationActivationHandler.ActivationArguments, string.Empty }
            };
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            
            _host = Host.CreateDefaultBuilder(e.Args)
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);
                        c.AddInMemoryCollection(activationArgs);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

            if (ToastNotificationManagerCompat.WasCurrentProcessToastActivated())
            {
                return;
            }

            await _host.StartAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // TODO: Register your services, viewmodels and pages here

            // App Host
            services.AddHostedService<ApplicationHostService>();
            services.AddSingleton<IIdentityCacheService, IdentityCacheService>();
            services.AddHttpClient("msgraph", client =>
            {
                client.BaseAddress = new System.Uri("https://graph.microsoft.com/v1.0/");
            });

            // Activation Handlers
            services.AddSingleton<IActivationHandler, ToastNotificationActivationHandler>();

            // Core Services
            services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>();
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddSingleton<IFileService, FileService>();

            // Services
            services.AddSingleton<IUserDataService, UserDataService>();
            services.AddSingleton<IToastNotificationsService, ToastNotificationsService>();
            services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
            services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<ISystemService, SystemService>();
            services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Views and ViewModels
            services.AddTransient<IShellWindow, ShellWindow>();
            services.AddTransient<ShellViewModel>();

            services.AddTransient<DashboardViewModel>();
            services.AddTransient<DashboardPage>();

            services.AddTransient<OrdersViewModel>();
            services.AddTransient<OrdersPage>();

            services.AddTransient<ProductsViewModel>();
            services.AddTransient<ProductsPage>();

            services.AddTransient<ServicesViewModel>();
            services.AddTransient<ServicesPage>();

            services.AddTransient<VetsViewModel>();
            services.AddTransient<VetsPage>();

            services.AddTransient<CustomersViewModel>();
            services.AddTransient<CustomersPage>();

            services.AddTransient<EmployeesViewModel>();
            services.AddTransient<EmployeesPage>();

            services.AddTransient<ReportsViewModel>();
            services.AddTransient<ReportsPage>();

            services.AddTransient<HelpViewModel>();
            services.AddTransient<HelpPage>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();

            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            _host = null;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
        }
    }
}
