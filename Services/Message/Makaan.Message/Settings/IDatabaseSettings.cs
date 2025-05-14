namespace Makaan.Message.Settings
{
    public interface IDatabaseSettings
    {
        public string ReceivedMessageCollectionName { get; set; }
        public string SenderedMessageCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
