namespace Makaan.Catalog.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string IntroTextAreaCollectionName { get; set; }
        public string IntroSliderAreaCollectionName { get; set; }
        public string PropertyTypeCollectionName { get; set; }
        public string FeaturedAboutCollectionName { get; set; }
        public string PropertyCollectionName { get; set; }
        public string PropertyAgentCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string PropertyIntroPosterCollectionName { get; set; }
        public string ContactIntroPosterCollectionName { get; set; }
        public string PropertyDetailCollectionName { get; set; }
        public string PropertyImageCollectionName { get; set; }
        public string EstateAgentApplicationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
