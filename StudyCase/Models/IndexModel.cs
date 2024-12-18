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

        public void OnGet()
        {
            var response = _elasticsearchService.GetLinksFromElasticsearch();
            Links = response.ToList();
        }
    }
}
