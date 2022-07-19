using System;
using DW.ELA.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;
namespace EliteLogAgent.Notification;

public class WindowsToastNotificationInterface : IUserNotificationInterface
{
    public void ShowErrorNotification(string error) =>
        new ToastContentBuilder()
            .AddText("Elite Log Agent: Error")
            .AddText(error)
            .Show();
}

// The Microsoft.Toolkit.Uwp.Notifications package only exposes the `Show`
// method if using a `net6.0-windows10.0.17763.0` TFM or later. Provide
// a dummy method so app can compile.
#if !WINDOWS10_1809_OR_GREATER
internal static class ToastContentBuilderExtensions
{
    internal static void Show(this ToastContentBuilder _) => throw new PlatformNotSupportedException();
}
#endif