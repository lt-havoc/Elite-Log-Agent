using System;
using System.Runtime.InteropServices;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Services.Logging.NLogIntegration;
using Castle.Windsor;
using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Utility;
using EliteLogAgent.Autorun;
using EliteLogAgent.Deployment;
using EliteLogAgent.Notification;
using EliteLogAgent.ViewModels;

namespace EliteLogAgent;

internal static partial class ContainerBootstrapper
{
    internal static void Initalize(IWindsorContainer container)
    {
        // Initalize infrastructure classes - NLog, Windsor
        container.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().ConfiguredExternally());
        container.Register(
            Component.For<IPathManager>().ImplementedBy<DataPathManager>().LifestyleSingleton(),
            Component.For<ISettingsProvider>().ImplementedBy<FileSettingsStorage>().LifestyleSingleton(),
            Component.For<ILogSettingsBootstrapper>().ImplementedBy<NLogSettingsManager>().LifestyleTransient(),
            Component.For<IPluginManager>().ImplementedBy<CastleWindsorPluginLoader>().LifestyleSingleton(),
            Component.For<IWindsorContainer>().Instance(container),
            Component.For<IRestClientFactory>().ImplementedBy<ThrottlingRestClient.Factory>());

        // Register core classes
        container.RegisterLogDirNameProvider();
        container.Register(
            Component.For<ILogRealTimeDataSource>().ImplementedBy<JournalMonitor>().LifestyleSingleton(),
            Component.For<IPlayerStateHistoryRecorder>().ImplementedBy<PlayerStateRecorder>().LifestyleSingleton());

        // Register UI classes. Need to initalize before log to enable tray icon
        container.RegisterNotificationInterface();

        // TODO: register autorun manager based on platform
        container.Register(Component.For<IAutorunManager>().ImplementedBy<ExternalAutorunManager>().LifestyleTransient());
        // Different components will be used based on whether apps are portable
        // if (AppInfo.IsNetworkDeployed)
        //     container.Register(Component.For<IAutorunManager>().ImplementedBy<ClickOnceAutorunManager>().LifestyleTransient());
        // else
        //     container.Register(Component.For<IAutorunManager>().ImplementedBy<PortableAutorunManager>().LifestyleTransient());

        container.RegisterViewModels();
    }

    private static void RegisterNotificationInterface(this IWindsorContainer container)
    {
        var @interface =
#if WINDOWS10_1809_OR_GREATER
            typeof(WindowsToastNotificationInterface);
#elif WINDOWS
            typeof(NoopUserNotificationService);
#elif LINUX
            typeof(FreeDesktopUserNotificationInterface);
#else
            throw new InvalidOperationException($"Attempted to register a notification interface for unsupported platform '{RuntimeInformation.OSDescription}'");
#endif

        container.Register(Component.For<IUserNotificationInterface>().ImplementedBy(@interface).LifestyleSingleton());
    }

    private static void RegisterViewModels(this IWindsorContainer container)
    {
        container.Register(Component.For<ApplicationViewModel>().ImplementedBy<ApplicationViewModel>().LifestyleTransient());
        container.Register(Component.For<MainWindowViewModel>().ImplementedBy<MainWindowViewModel>().LifestyleTransient());
    }

    private static void RegisterLogDirNameProvider(this IWindsorContainer container)
    {
        var provider =
#if WINDOWS
            typeof(WindowsSavedGamesDirectoryProvider);
#elif LINUX
            typeof(LinuxSavedGamesDirectoryProvider);
#else
            throw new InvalidOperationException($"Attempted to register a log directory name provider for unsupported platform '{RuntimeInformation.OSDescription}'");
#endif

        container.Register(Component.For<ILogDirectoryNameProvider>().ImplementedBy(provider).LifestyleSingleton());
    }
}