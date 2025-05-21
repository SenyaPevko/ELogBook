namespace Domain.Settings;

public static class SignalrSettings
{
    public const string NotificationHubPath = "/hubs/notifications";

    public static class ClientMethods
    {
        public const string ReceiveNotification = "ReceiveNotification";
    }
}