using StudyCase.Models;

namespace StudyCase.Services.LinkProcessingService
{
    public interface ILinkProcessingService
    {
        List<string> FilterLinks(List<string> links, string search);
        (List<string> PagedLinks, int TotalPages) GetPagedLinks(List<string> links, int page, int pageSize);
        (List<string> PagedLinks, int TotalPages) GetPagedLinksWithFiltering(PaginationModel paginationModel);
    }
}
