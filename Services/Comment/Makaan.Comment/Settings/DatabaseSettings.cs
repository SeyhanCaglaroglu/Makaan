namespace Makaan.Comment.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string MemberCommentCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
