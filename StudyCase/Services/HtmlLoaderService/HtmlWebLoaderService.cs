using HtmlAgilityPack;

namespace StudyCase.Services.HtmlLoaderService
{
    public class HtmlWebLoaderService : IHtmlLoaderService
    {
        private readonly HttpClient _httpClient;

        public HtmlWebLoaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HtmlDocument> LoadAsync(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            return doc;
        }
    }
}
