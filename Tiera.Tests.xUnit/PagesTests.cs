using System.IO;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Tiera.Contracts.Services;
using Tiera.Core.Contracts.Services;
using Tiera.Core.Services;
using Tiera.Models;
using Tiera.Services;
using Tiera.ViewModels;
using Tiera.Views;

using Xunit;

namespace Tiera.Tests.XUnit
{
    public class PagesTests
    {
        private readonly IHost _host;

        public PagesTests()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c => c.SetBasePath(appLocation))
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Core Services
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IIdentityService, IdentityService>();
            services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>();

            // Services
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<ISystemService, SystemService>();
            services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
            services.AddSingleton<IUserDataService, UserDataService>();
            services.AddSingleton<IIdentityCacheService, IdentityCacheService>();
            services.AddHttpClient("msgraph", client =>
            {
                client.BaseAddress = new System.Uri("https://graph.microsoft.com/v1.0/");
            });
            services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModels
            services.AddTransient<VetsViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<ServicesViewModel>();
            services.AddTransient<ReportsViewModel>();
            services.AddTransient<ProductsViewModel>();
            services.AddTransient<OrdersViewModel>();
            services.AddTransient<HelpViewModel>();
            services.AddTransient<EmployeesViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<CustomersViewModel>();

            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }

        // TODO WTS: Add tests for functionality you add to VetsViewModel.
        [Fact]
        public void TestVetsViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(VetsViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetVetsPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(VetsViewModel).FullName);
                Assert.Equal(typeof(VetsPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to SettingsViewModel.
        [Fact]
        public void TestSettingsViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(SettingsViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetSettingsPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(SettingsViewModel).FullName);
                Assert.Equal(typeof(SettingsPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to ServicesViewModel.
        [Fact]
        public void TestServicesViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(ServicesViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetServicesPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(ServicesViewModel).FullName);
                Assert.Equal(typeof(ServicesPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to ReportsViewModel.
        [Fact]
        public void TestReportsViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(ReportsViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetReportsPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(ReportsViewModel).FullName);
                Assert.Equal(typeof(ReportsPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to ProductsViewModel.
        [Fact]
        public void TestProductsViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(ProductsViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetProductsPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(ProductsViewModel).FullName);
                Assert.Equal(typeof(ProductsPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to OrdersViewModel.
        [Fact]
        public void TestOrdersViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(OrdersViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetOrdersPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(OrdersViewModel).FullName);
                Assert.Equal(typeof(OrdersPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to HelpViewModel.
        [Fact]
        public void TestHelpViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(HelpViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetHelpPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(HelpViewModel).FullName);
                Assert.Equal(typeof(HelpPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to EmployeesViewModel.
        [Fact]
        public void TestEmployeesViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(EmployeesViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetEmployeesPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(EmployeesViewModel).FullName);
                Assert.Equal(typeof(EmployeesPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to DashboardViewModel.
        [Fact]
        public void TestDashboardViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(DashboardViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetDashboardPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(DashboardViewModel).FullName);
                Assert.Equal(typeof(DashboardPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }

        // TODO WTS: Add tests for functionality you add to CustomersViewModel.
        [Fact]
        public void TestCustomersViewModelCreation()
        {
            var vm = _host.Services.GetService(typeof(CustomersViewModel));
            Assert.NotNull(vm);
        }

        [Fact]
        public void TestGetCustomersPageType()
        {
            if (_host.Services.GetService(typeof(IPageService)) is IPageService pageService)
            {
                var pageType = pageService.GetPageType(typeof(CustomersViewModel).FullName);
                Assert.Equal(typeof(CustomersPage), pageType);
            }
            else
            {
                Assert.True(false, $"Can't resolve {nameof(IPageService)}");
            }
        }
    }
}
