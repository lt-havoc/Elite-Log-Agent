namespace EliteLogAgent.Notification
{
    using DW.ELA.Interfaces;

    public class NoopUserNotificationService : IUserNotificationInterface
    {
        public void ShowErrorNotification(string error) { }
    }
}