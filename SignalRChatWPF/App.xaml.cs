using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SignalRChatWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<SignalRChat.Startup>())
                .ConfigureServices(services =>
                {
                    services.AddTransient<MainWindow>();
                })
                .Build();

            _host.Start();

            _host.Services.GetRequiredService<MainWindow>().Show();
        }

        protected override void OnExit(ExitEventArgs e) => _host.Dispose();
    }
}
