using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KW_Sales_UI.Services;
using KW_Sales_UI.ViewModels;
using KW_Sales_UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace KW_Sales_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddTransient<OrderViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDataConnectionStringService, DataConnectionStringService>();

            services.AddSingleton<Func<Type, BaseViewModel>>(ServiceProvider => viewModelType => (BaseViewModel)ServiceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainView = _serviceProvider.GetRequiredService<MainView>();
            mainView.Show();
            base.OnStartup(e);
        }
    }
}
