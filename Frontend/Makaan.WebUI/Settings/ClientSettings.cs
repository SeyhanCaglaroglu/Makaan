namespace Makaan.WebUI.Settings
{
    public class ClientSettings
    {
        public Client Makaan_Visitor { get; set; }
        public Client Makaan_Admin { get; set; }
        public Client Makaan_Member { get; set; }
        public Client Makaan_EstateAgent { get; set; }
        public class Client
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }
    }
}
