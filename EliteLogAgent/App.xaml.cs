namespace EliteLogAgent;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Castle.Windsor;
using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Utility;
using NLog;
using NLog.Fluent;
using ViewModels;
using Views;

public class App : Application
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
    private readonly List<IDisposable> disposables = new List<IDisposable>();

    public override void Initialize()
    {
        AppDomain.CurrentDomain.ProcessExit += Shutdown;
        // Since we handle the process exit event, we will also take care of shutting down
        // the logger. This allows us to log in the exit event.
        LogManager.AutoShutdown = false;
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var (viewModel, pluginManager) = Configure();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = new MainWindow { DataContext = viewModel };
            window.DataTemplates.Add(new PluginViewLocator(pluginManager.LoadedPlugins));
            window.DataTemplates.Add(new ViewLocator());
#if !DEBUG
            window.Opened += (sender, args) => (sender as MainWindow)!.WindowState = WindowState.Minimized;
#endif
            desktop.MainWindow = window;
            desktop.Exit += Shutdown;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private (MainWindowViewModel, IPluginManager) Configure()
    {
        var container = new WindsorContainer();
        ContainerBootstrapper.Initalize(container);

        // Setup logs
        container.Resolve<ILogSettingsBootstrapper>().Setup();
        Log.Info()
            .Message("Application started")
            .Property("version", AppInfo.Version)
            .Write();

        // Load plugins
        var pluginManager = container.Resolve<IPluginManager>();
        pluginManager.LoadPlugin("DW.ELA.Plugin.Inara");
        pluginManager.LoadPlugin("DW.ELA.Plugin.EDDN");
        pluginManager.LoadPlugin("DW.ELA.Plugin.EDSM");
        pluginManager.LoadEmbeddedPlugins();

        var logMonitor = container.Resolve<ILogRealTimeDataSource>();
        //var trayController = container.Resolve<IUserNotificationInterface>();
        var playerStateRecorder = container.Resolve<IPlayerStateHistoryRecorder>();

        // log monitor needs to get disposed first to ensure every plugin gets 'OnCompleted' event
        // subscription 'token' is IDisposable
        // subscribing the PlayerStateRecorder first to avoid potential issues with out-of-order execution because of threading
        disposables.Insert(0, logMonitor);
        disposables.Add(new CompositeDisposable(pluginManager.LoadedPlugins.Select(p => logMonitor.Subscribe(p.GetLogObserver()))));
        disposables.Add(logMonitor.Subscribe(playerStateRecorder));
        disposables.Add(container);

        return (container.Resolve<MainWindowViewModel>(), pluginManager);
    }

    private void Shutdown(object? sender, EventArgs e)
    {
        foreach (var disposable in disposables)
            disposable.Dispose();

        Log.Info("Shutting down");
        LogManager.Shutdown();
    }
}