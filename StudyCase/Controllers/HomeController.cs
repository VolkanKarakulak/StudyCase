using Microsoft.AspNetCore.Mvc;
using StudyCase.Models;
using StudyCase.Services.LinkProcessingService;
using StudyCase.ViewModels;


namespace StudyCase.Controllers
{
    public class HomeController : Controller
    {
    
        private readonly ILinkProcessingService _linkProcessingService;

        public HomeController(ILinkProcessingService linkProcessingService)
        {
            _linkProcessingService = linkProcessingService;   
        }

        public async Task<ActionResult> Index(PaginationModel paginationModel)
        {

            var (pagedLinks, totalPages) = await _linkProcessingService.GetPagedLinksWithFiltering(paginationModel);

            var viewModel = new PagedLinksViewModel
            {
                Links = pagedLinks,
                CurrentPage = paginationModel.Page,
                TotalPages = totalPages,
                SearchQuery = paginationModel.Search,
                PageCount = pagedLinks.Count,
            };

            return View(viewModel);
        }

    }
}
