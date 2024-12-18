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
            // G�reli URL'yi tan�mlay�n (�rnek olarak)
            string relativeUrl = "/";  // Bu URL'yi dinamik olarak alabilirsiniz
            string baseUrl = "https://www.sozcu.com.tr";

            // Tam URL'yi olu�turun
            string fullUrl = new Uri(new Uri(baseUrl), relativeUrl).ToString();

            List<string> allLinks = _webCrawlerService.GetLinks(fullUrl);

            if (!string.IsNullOrEmpty(search))
            {
                allLinks = allLinks.Where(link => link.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sayfalama i�in veriyi dilimleme
            var pagedLinks = allLinks.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Sayfa ba��na ka� ��e oldu�unu, toplam ��e say�s�n� ve ge�erli sayfay� ViewData ile g�nderiyoruz
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)allLinks.Count / pageSize);
            ViewData["TotalCount"] = allLinks.Count;
            ViewData["Search"] = search;

            return View(pagedLinks);
        }

    }
}
