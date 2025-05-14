namespace Makaan.WebUI.Settings
{
    public class ServiceApiSettings
    {
        public string OcelotUrl { get; set; }
        public string IdentityServerUrl { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi Comment { get; set; }
        public ServiceApi Notification { get; set; }
        public ServiceApi Message { get; set; }

        public class ServiceApi
        {
            public string Path { get; set; }
        }
    }
}
