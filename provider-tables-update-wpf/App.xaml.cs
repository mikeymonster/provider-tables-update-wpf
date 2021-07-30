using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using provider_tables_update_wpf.ViewModels;
using System;
using System.Windows;

namespace provider_tables_update_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public IConfiguration Configuration { get; private set; }

        public App() => 
            _host = new HostBuilder()
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder
                        .SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<AppSettings>(context.Configuration);

                    //services.AddSingleton<ITextService, TextService>();
                    services.AddTransient(typeof(MainViewModel));
                    services.AddSingleton<MainWindow>();
                })
                .Build();

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow!.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    //services.Configure<AppSettings>(context.Configuration);
        //    services.Configure<AppSettings>
        //        (Configuration.GetSection(nameof(AppSettings)));
        //    //services.AddSingleton(typeof(MainViewModel));
        //    services.AddTransient(typeof(MainViewModel));
        //    services.AddTransient(typeof(MainWindow));
        //}
    }
}
