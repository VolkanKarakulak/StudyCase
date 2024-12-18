using Nest;

namespace StudyCase.Services.ElasticsearchService
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("sozcu_links");
            _client = new ElasticClient(settings);
        }
        public void SaveLinks(List<string> links)
        {
            var bulkRequest = new BulkRequest
            {
                Operations = links.Select(link => new BulkIndexOperation<string>(link)).Cast<IBulkOperation>().ToList()
            };

            var response = _client.Bulk(bulkRequest);

            if (response.Errors)
            {
                Console.WriteLine("Bulk index operation failed.");
            }
            else
            {
                Console.WriteLine("Data successfully indexed in Elasticsearch.");
            }
        }

        // Elasticsearch'ten bağlantıları çekme (arama yaparak)
        public List<string> GetLinksFromElasticsearch(string search = null)
        {
            var searchDescriptor = new SearchDescriptor<string>()
                .Query(q => q.MatchAll()); // Varsayılan olarak tüm veriler

            // Eğer arama yapılmışsa, match sorgusu ekleyelim
            if (!string.IsNullOrEmpty(search))
            {
                searchDescriptor = searchDescriptor.Query(q => q.Match(m => m.Field("_all").Query(search)));
            }

            // Elasticsearch'ü sorgula
            var response = _client.Search<string>(s => searchDescriptor);

            // Sonuçları döndür
            return response.Documents.ToList();
        }
    }
}
