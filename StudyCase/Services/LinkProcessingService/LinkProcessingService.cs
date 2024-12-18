using StudyCase.Models;
using StudyCase.Services.SozcuService;

namespace StudyCase.Services.LinkProcessingService
{
    public class LinkProcessingService: ILinkProcessingService
    {
        private readonly ISozcuService _sozcuService;

        public LinkProcessingService(ISozcuService sozcuService)
        {
            _sozcuService = sozcuService;
        }
        public async Task<List<string>> FilterLinks(List<string> links, string search)
        {
            if (string.IsNullOrEmpty(search)) return links;

            return links.Where(link => link.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        public async Task<(List<string> PagedLinks, int TotalPages)> GetPagedLinksWithFiltering(PaginationModel paginationModel)
        {
            // Get all links from the web
            List<string> allLinks = await _sozcuService.GetLinksFromSozcu();

            // Filter the links based on the search term
            allLinks = await FilterLinks(allLinks, paginationModel.Search);

            // Calculate pagination
            var (pagedLinks, totalPages) = await GetPagedLinks(allLinks, paginationModel.Page, paginationModel.PageSize);

            return (pagedLinks, totalPages);
        }

        public async Task<(List<string> PagedLinks, int TotalPages)> GetPagedLinks(List<string> links, int page, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((double)links.Count / pageSize);
            var pagedLinks = links.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return await Task.FromResult((pagedLinks, totalPages));
        }
    }
}
