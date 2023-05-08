using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using Autofac;
using CommunityToolkit.Mvvm.Messaging;
using Kafo.Desktop.AppLayer.Authorization;
using Kafo.Desktop.AppLayer.Utilities;
using Kafo.Desktop.AppLayer.Utilities.Models;
using Kafo.Desktop.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Kafo.Desktop.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public IContainer ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnException);


        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        var containerBuilder = new ContainerBuilder();
        ConfigureServices(containerBuilder);

        ServiceProvider = containerBuilder.Build();
        DISource.Resolver = ServiceProvider.Resolve;

        var mainWindow = ServiceProvider.Resolve<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(ContainerBuilder builder)
    {
        builder.RegisterType<MainWindow>().SingleInstance();
        builder.Register(c => Options.Create(Configuration.GetSection("NetworkOptions")
            .Get<AuthenticatedHttpClientOptions>()));


        builder.RegisterAssemblyTypes(typeof(App).Assembly)
            .Where(t => t.Name.EndsWith("ViewModel"))
            .AsSelf()
            .AsImplementedInterfaces();
        builder.RegisterAssemblyTypes(typeof(UserInfo).Assembly)
            .Where(t => t.Name.EndsWith("Query"))
            .AsSelf();
        builder.RegisterAssemblyTypes(typeof(UserInfo).Assembly)
            .Where(t => t.Name.EndsWith("Command"))
            .AsSelf();

        builder.RegisterType<UserInfo>().SingleInstance();
        builder.RegisterType<AppNavigator>().SingleInstance();
        builder.RegisterType<StrongReferenceMessenger>().As<IMessenger>().SingleInstance();

        builder.RegisterType<AuthenticatedHttpClient>()
            .AsSelf()
            .SingleInstance();    }


    private void OnException(object sender, UnhandledExceptionEventArgs args)
    {
        MessageBox.Show(args.ExceptionObject.ToString());
    }
}