namespace Makaan.Message.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ReceivedMessageCollectionName { get; set; }
        public string SenderedMessageCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
