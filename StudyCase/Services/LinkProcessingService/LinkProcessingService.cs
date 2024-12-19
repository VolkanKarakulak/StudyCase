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
            // Eğer arama terimi boşsa, verilen linkleri olduğu gibi döndürür.
            if (string.IsNullOrEmpty(search)) return links;

            // Arama terimi linklerde geçiyorsa, bu linkleri küçük/büyük harf duyarsız döndürür.
            var filterlinks = links.Where(link => link.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
           
            return filterlinks;
        }

        // Sayfalama ve filtreleme işlemi
        public async Task<(List<string> PagedLinks, int TotalPages)> GetPagedLinksWithFiltering(PaginationModel paginationModel)
        {
            // Sozcu'dan tüm linkleri alır
            List<string> allLinks = await _sozcuService.GetLinksFromSozcu();

            // Arama terimine göre linkleri filtreler
            allLinks = await FilterLinks(allLinks, paginationModel.Search);

            // Sayfalama hesaplaması yapar
            var (pagedLinks, totalPages) = await GetPagedLinks(allLinks, paginationModel.Page, paginationModel.PageSize);

            return (pagedLinks, totalPages);
        }

        // Verilen link listesini sayfalama işlemi
        public async Task<(List<string> PagedLinks, int TotalPages)> GetPagedLinks(List<string> links, int page, int pageSize)
        {
            // Sayfa sayısını hesaplar (toplam link sayısı / sayfa başına link sayısı)
            int totalPages = (int)Math.Ceiling((double)links.Count / pageSize);

            // Verilen sayfa numarası ve sayfa boyutuna göre linkleri alır.
            var pagedLinks = links.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            // Sayfalı linkler ve toplam sayfa sayısını döndürür.
            return await Task.FromResult((pagedLinks, totalPages));
        }
    }
}
