using Microsoft.AspNetCore.Mvc;
using StudyCase.Models;
using StudyCase.Services.LinkProcessingService;
using StudyCase.Services.SozcuService;
using StudyCase.Services.WebCrawlerService;
using System.Diagnostics;

namespace StudyCase.Controllers
{
    public class HomeController : Controller
    {
    

        private readonly ILinkProcessingService _linkProcessingService;

 

        public HomeController(ILinkProcessingService linkProcessingService)
        {
            _linkProcessingService = linkProcessingService;
    
        }

        public ActionResult Index(PaginationModel paginationModel)
        {

            var (pagedLinks, totalPages) = _linkProcessingService.GetPagedLinksWithFiltering(paginationModel);

            // Sayfa ba��na ka� ��e oldu�unu, toplam ��e say�s�n� ve ge�erli sayfay� ViewData ile g�nderiyoruz
            ViewData["CurrentPage"] = paginationModel.Page;
            ViewData["TotalPages"] = totalPages;
            ViewData["TotalCount"] = pagedLinks.Count;
            ViewData["Search"] = paginationModel.Search;
           
            return View(pagedLinks);
        }

    }
}
