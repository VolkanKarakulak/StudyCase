namespace StudyCase.ViewModels
{
    public class PagedLinksViewModel
    {
        public List<string>? Links { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageCount { get; set; }
        public string? SearchQuery { get; set; }
    }
}
