namespace StudyCase.Services.WebCrawlerService
{
    public interface IWebCrawlerService
    {
         Task<List<string>> GetLinks(string url);
    }
}
