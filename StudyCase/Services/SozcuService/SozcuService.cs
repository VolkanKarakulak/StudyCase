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
            _sozcuSettings = sozcuSettings.Value; // SozcuSettings'leri alıyoruz
        }


        public List<string> GetLinksFromSozcu()
        {
            string fullUrl = new Uri(new Uri(_sozcuSettings.BaseUrl), "/").ToString();
            return _webCrawlerService.GetLinks(fullUrl);
        }

    }
}
