using Elasticsearch.Net;
using Nest;
using Search.API.Models;

namespace Search.API.Extensions
{
    public static class ElasticSearchExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            //var index = "News";
            //var uri = new Uri(configuration["Elasticsearch:Url"]);
            //var connectionPool = new SingleNodeConnectionPool(uri);
            //var settings = new ConnectionSettings(connectionPool);
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));

            settings.DefaultIndex("news");
            settings.DefaultMappingFor<News>(m => m
                .IndexName("news"));

            var client = new ElasticClient(settings);

            
        }
        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<News>(m => m);
        }
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName, index => index.Map<News>(x => x.AutoMap()));
        }
    }
}
