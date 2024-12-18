using StudyCase.Models;

namespace StudyCase.Services.LinkProcessingService
{
    public interface ILinkProcessingService
    {
        Task<List<string>> FilterLinks(List<string> links, string search);
        Task<(List<string> PagedLinks, int TotalPages)> GetPagedLinks(List<string> links, int page, int pageSize);
        Task<(List<string> PagedLinks, int TotalPages)> GetPagedLinksWithFiltering(PaginationModel paginationModel);
    }
}
