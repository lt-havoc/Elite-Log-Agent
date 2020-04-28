namespace EliteLogAgent
{
    using System;
    using Avalonia;
    using Avalonia.Logging.Serilog;
    using NLog;

    class Program
    {
        private static readonly ILogger RootLog = LogManager.GetCurrentClassLogger();
        
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            if (SingleLaunch.IsRunning)
                return; // only one instance should be running
            
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();
        
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message = e.IsTerminating ? "Unhandled fatal exception" : "Unhandled exception";
            var senderString = sender?.GetType()?.ToString() ?? "unknown";
            var exceptionTypeString = e.ExceptionObject?.GetType()?.ToString() ?? "unknown";
            var exceptionObjectString = e.ExceptionObject?.ToString() ?? "unknown";

            if (e.ExceptionObject is Exception ex)
                RootLog.Fatal(ex, message + " from {0}", senderString);
            else
                RootLog.Fatal(message + " of unknown type: {0} {1}", exceptionTypeString, exceptionObjectString);
        }
    }
}
