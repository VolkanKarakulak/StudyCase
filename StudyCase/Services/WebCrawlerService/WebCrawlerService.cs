
using Microsoft.Extensions.Options;
using StudyCase.Configuration;
using StudyCase.Services.HtmlLoaderService;

namespace StudyCase.Services.WebCrawlerService
{
    // WebCrawlerService, web sayfasındaki bağlantıları çıkartmak için kullanılan bir servistir.
    public class WebCrawlerService : IWebCrawlerService
    {
        // HtmlLoaderService servisi, URL'den HTML içeriğini yüklemek için kullanılır.
        private readonly IHtmlLoaderService _htmlLoader;
        private readonly SozcuSettings _sozcuSettings;

        public WebCrawlerService(IHtmlLoaderService htmlLoader, IOptions<SozcuSettings> sozcuSettings)
        {
            _htmlLoader = htmlLoader;
            _sozcuSettings = sozcuSettings.Value;
        }

        // GetLinks metodu, verilen bir URL'deki tüm bağlantıları çıkartır.
        public async Task<List<string>> GetLinks(string url)
        {
            try
            {
                // HTML içeriğini yükler ve HtmlDocument nesnesi döndürür.
                var document = await _htmlLoader.LoadAsync(url);

                // HTML içeriğindeki tüm <a> etiketlerini seçer (bağlantıları alır)
                var nodes = document.DocumentNode.SelectNodes("//a");

                if (nodes != null)
                {
                    // Temel URL, SozcuSettings sınıfından alınıyor.
                    string baseUrl = _sozcuSettings.BaseUrl;

                    // Bağlantıları döndürmeden önce her birini kontrol eder ve tam URL'lere dönüştürür.
                    var links = nodes.Select(node =>
                    {
                        // HTML öğesinin href özelliğini almak için Attributes koleksiyonundan href anahtarına erişir.
                        var href = node.Attributes["href"]?.Value;

                        //// Eğer "href" boş veya null ise geçerli bir bağlantı yok demektir, o yüzden null döndürülür.
                        if (string.IsNullOrEmpty(href)) return null;

                        // Eğer URL tam bir URL (absolute) değilse, yerel bir URL'dir.
                        // Bu durumda yerel URL'yi tam URL'ye dönüştürmek için "baseUrl" kullanılır.
                        if (!Uri.IsWellFormedUriString(href, UriKind.Absolute))
                        {
                            // Yerel URL'yi tam URL'ye dönüştür
                            //href = new Uri(new Uri(baseUrl), href).ToString();

                            // Base URL'nin doğru formatta olduğunu kontrol edip ve Uri nesnesine dönüştürür.
                            Uri baseUri = new Uri(baseUrl);

                            // Göreli URL (href) ile tam URL oluşturur.
                            Uri fullUri = new Uri(baseUri, href);

                            // Tam URL'yi string formatına dönüştürür.
                            href = fullUri.ToString();
                        }

                        return href;
                    })
                    .Where(link => !string.IsNullOrEmpty(link)) // Null veya boş olmayanlar döner.
                    .ToList();

                    // Aynı bağlantılardan birden fazla olmaması için Distinct() kullanılmıştır.
                    var uniqueLinks = links.Distinct().ToList();  

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
