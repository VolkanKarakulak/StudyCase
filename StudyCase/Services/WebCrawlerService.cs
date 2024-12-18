using HtmlAgilityPack;
using StudyCase.Services.HtmlLoaderService;

namespace StudyCase.Services
{
    public class WebCrawlerService
    {
        private readonly IHtmlLoader _htmlLoader;

        public WebCrawlerService(IHtmlLoader htmlLoader)
        {
            _htmlLoader = htmlLoader;
        }
        public List<string> GetLinks(string url)
        {
            try
            {
                var document = _htmlLoader.Load(url);
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
                // Hata durumunda loglama veya işlemleri yap
                Console.WriteLine($"Error fetching links from {url}: {ex.Message}");
                return new List<string>();
            }
        }

    }
}
