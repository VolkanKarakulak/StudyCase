using Microsoft.AspNetCore.Mvc;
using StudyCase.Models;
using StudyCase.Services;
using System.Diagnostics;

namespace StudyCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebCrawlerService _webCrawlerService;

        public HomeController(ILogger<HomeController> logger,  WebCrawlerService webCrawlerService)
        {
            _logger = logger;
            _webCrawlerService = webCrawlerService;
        }

        public ActionResult Index(string search,int page = 1, int pageSize = 10)
        {
            // Göreli URL'yi tanýmlayýn (örnek olarak)
            string relativeUrl = "/";  // Bu URL'yi dinamik olarak alabilirsiniz
            string baseUrl = "https://www.sozcu.com.tr";

            // Tam URL'yi oluþturun
            string fullUrl = new Uri(new Uri(baseUrl), relativeUrl).ToString();

            List<string> allLinks = _webCrawlerService.GetLinks(fullUrl);

            if (!string.IsNullOrEmpty(search))
            {
                allLinks = allLinks.Where(link => link.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sayfalama için veriyi dilimleme
            var pagedLinks = allLinks.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Sayfa baþýna kaç öðe olduðunu, toplam öðe sayýsýný ve geçerli sayfayý ViewData ile gönderiyoruz
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)allLinks.Count / pageSize);
            ViewData["TotalCount"] = allLinks.Count;
            ViewData["Search"] = search;

            return View(pagedLinks);
        }

    }
}
