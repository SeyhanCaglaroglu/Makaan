namespace Makaan.Notification.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CommentNotificationCollectionName { get; set; }
        public string ApplicationNotificationCollectionName { get; set; }
        public string AccountNotificationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
