using Microsoft.Extensions.Options;
using StudyCase.Configuration;
using StudyCase.Services.WebCrawlerService;

namespace StudyCase.Services.SozcuService
{
    public class SozcuService : ISozcuService
    {
        private readonly IWebCrawlerService _webCrawlerService;
        private readonly SozcuSettings _sozcuSettings;

        public SozcuService(IWebCrawlerService webCrawlerService, IOptions<SozcuSettings> sozcuSettings)
        {
            _webCrawlerService = webCrawlerService;
            _sozcuSettings = sozcuSettings.Value;
        }

        // Web sayfasındaki linkleri almak için kullanılıyor.
        public async Task<List<string>> GetLinksFromSozcu()
        {
            //string fullUrl = new Uri(new Uri(_sozcuSettings.BaseUrl), "/").ToString();
       

            // Temel URL'yi SozcuSettings sınıfından alıyoruz
            string baseUrl = _sozcuSettings.BaseUrl;

            // BaseUrl bir Uri nesnesine dönüştürülüyor
            Uri baseUri = new Uri(baseUrl);

            // Şimdi "/" ile birleştirilen yeni bir URI oluşturuluyor
            //  "/" tek başına, kök dizini ifade eder. Yani, baseUri'nin köküne (yani, domain kısmına) işaret ede
            Uri fullUri = new Uri(baseUri, "/");

            // Son olarak, bu birleşik URI'yi bir string'e dönüştürüyoruz
            string fullUrl = fullUri.ToString();

            // Ve bu tam URL'yi GetLinks metoduna gönderiyoruz
            return await _webCrawlerService.GetLinks(fullUrl);

        }

    }
}
