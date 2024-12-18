namespace StudyCase.Services.WebCrawlerService
{
    public interface IWebCrawlerService
    {
         List<string> GetLinks(string url);
    }
}
