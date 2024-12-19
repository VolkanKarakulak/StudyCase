using Nest;

namespace StudyCase.Services.ElasticsearchService
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly ElasticClient _client;

        public ElasticsearchService()
        {
            // Elasticsearch bağlantı ayarlarını yapıyoruz, localhost:9200 adresine bağlanıyoruz.
            // Varsayılan indeks "website_links" olarak belirleniyor.
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("website_links");
            _client = new ElasticClient(settings); // ElasticClient nesnesi oluşturuluyor.
        }// Verilen bağlantıları Elasticsearch'e kaydeder.

        // Verilen bağlantıları Elasticsearch'e kaydeder.
        public async Task<string> SaveLinks(List<string> links)
        {
            // BulkRequest, topluca veri eklemek için kullanılır.
            var bulkRequest = new BulkRequest
            {
                // Verilen bağlantıları her biri için BulkIndexOperation ile Elasticsearch'e ekliyoruz.
                Operations = links.Select(link => new BulkIndexOperation<string>(link)).Cast<IBulkOperation>().ToList()
            }; 
            
            // Bulk isteği gönderiliyor
            var response = _client.Bulk(bulkRequest);

            if (response.Errors)
            {
                return "Data indexing failed.";
            }
            
            return "Data successfully indexed in Elasticsearch.";
        }

        // Elasticsearch'ten arama yaparak veya tüm verileri getirerek bağlantıları çeker.
        public async Task<List<string>> GetLinksFromElasticsearch(string search = null)
        {
            // Varsayılan olarak tüm verilerle sorgulama yapılıyor.
            var searchDescriptor = new SearchDescriptor<string>()
                .Query(q => q.MatchAll());

            // Eğer arama yapılmışsa, "_all" alanında arama yapılacak şekilde sorgu eklenir.
            if (!string.IsNullOrEmpty(search))
            {
                // Eğer arama yapılmışsa, "_all" alanında arama yapılacak şekilde sorgu eklenir.
                searchDescriptor = searchDescriptor.Query(q => q.Match(m => m.Field("_all").Query(search)));
            }

            // Elasticsearch'ü sorgula
            var response = _client.Search<string>(s => searchDescriptor);

            // Sorgu sonucunda dönen belgeler (documents) bir listeye dönüştürülüp geri döndürülür.
            return response.Documents.ToList();
        }
    }
}
