using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyCase.Services.ElasticsearchService;

namespace StudyCase.Models
{
    public class IndexModel : PageModel
    {
        private readonly ElasticsearchService _elasticsearchService;

        public IndexModel(ElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        public List<string> Links { get; set; }

        public async Task OnGet()
        {
            var response = await _elasticsearchService.GetLinksFromElasticsearch();
            Links = response.ToList();
        }
    }
}
