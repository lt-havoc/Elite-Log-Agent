using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace EliteLogAgent.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DW.ELA.Interfaces;
    using DW.ELA.Utility;
    using Tmds.DBus;

    public class FreeDesktopUserNotificationInterface : IUserNotificationInterface
    {
        private readonly INotifications notifications;
        private readonly string[] actions;
        private readonly Dictionary<string, object> hints;

        public FreeDesktopUserNotificationInterface()
        {
            notifications = Connection.Session.CreateProxy<INotifications>("org.freedesktop.Notifications", "/org/freedesktop/Notifications");
            actions = Array.Empty<string>();
            hints = new Dictionary<string, object>();
        }

        public void ShowErrorNotification(string error) =>
            notifications.NotifyAsync(AppInfo.Name, 0, "elite-log-agent", $"{AppInfo.Name} Error", error, actions, hints, 0);
    }

    /// <summary>
    /// Represents the desktop notifications specification
    /// https://developer.gnome.org/notification-spec/
    /// </summary>
    /// <remarks>
    /// This interface was generated via `dotnet dbus codegen --service org.freedesktop.Notifications`
    /// See https://github.com/tmds/Tmds.DBus
    /// </remarks>
    [DBusInterface("org.freedesktop.Notifications")]
    interface INotifications : IDBusObject
    {
        Task<uint> NotifyAsync(string appName, uint replaceId, string icon, string summary, string body, string[] actions, IDictionary<string, object> hints, int timeout);
        Task CloseNotificationAsync(uint id);
        Task<string[]> GetCapabilitiesAsync();
        Task<(string arg0, string arg1, string arg2, string arg3)> GetServerInformationAsync();
        Task<IDisposable> WatchNotificationClosedAsync(Action<(uint arg0, uint arg1)> handler, Action<Exception>? onError = null);
        Task<IDisposable> WatchActionInvokedAsync(Action<(uint arg0, string arg1)> handler, Action<Exception>? onError = null);
    }
}