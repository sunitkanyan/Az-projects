namespace AZ_AD_graph_api.helpers
{
    public class ConfigReader
    {
        public string TenantId;
        public string ClientId;
        public string ClientSecret;
        public string ObjectIdApp;


        public ConfigReader()
       {
            var config = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection("AppDetail");
            var graphClientConfig = section.Get<GraphServiceClientConfig>();
            ObjectIdApp = graphClientConfig.ObjectIdApp;
            TenantId = graphClientConfig.TenantId;
            ClientId = graphClientConfig.ClientId;
            ClientSecret =  graphClientConfig.ClientSecret;
        }
        private class GraphServiceClientConfig
        {
            public string TenantId { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string ObjectIdApp { get; set; }
        }

    }
   
}
