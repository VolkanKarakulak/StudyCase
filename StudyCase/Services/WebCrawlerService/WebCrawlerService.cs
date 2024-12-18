using HtmlAgilityPack;
using StudyCase.Services.HtmlLoaderService;

namespace StudyCase.Services.WebCrawlerService
{
    public class WebCrawlerService : IWebCrawlerService
    {
        private readonly IHtmlLoaderService _htmlLoader;

        public WebCrawlerService(IHtmlLoaderService htmlLoader)
        {
            _htmlLoader = htmlLoader;
        }
        public async Task<List<string>> GetLinks(string url)
        {
            try
            {
                var document = await _htmlLoader.LoadAsync(url);
                var nodes = document.DocumentNode.SelectNodes("//a");

                if (nodes != null)
                {
                    // Temel URL
                    string baseUrl = "https://www.sozcu.com.tr";

                    // Bağlantıları tam URL'lere dönüştürme
                    var links = nodes.Select(node =>
                    {
                        var href = node.Attributes["href"]?.Value;

                        // Eğer href null veya boşsa, geçerli URL'yi döndür
                        if (string.IsNullOrEmpty(href)) return null;

                        // Göreli URL'yi tam URL'ye dönüştür
                        if (!Uri.IsWellFormedUriString(href, UriKind.Absolute))
                        {
                            // Göreli URL'yi tam URL'ye dönüştür
                            href = new Uri(new Uri(baseUrl), href).ToString();
                        }

                        return href;
                    })
                    .Where(link => !string.IsNullOrEmpty(link)) // Null veya boş olmayanları al
                    .ToList();

                    // Benzersiz bağlantıları almak için Distinct() kullanın
                    var uniqueLinks = links.Distinct().ToList();  // Distinct() burada çalışacak

                    return uniqueLinks;
                }

                return new List<string>();
            }
            catch (Exception ex)
            {
                // Hata durumunda sadece boş bir liste dön
                return new List<string>();
            }
        }



    }
}
