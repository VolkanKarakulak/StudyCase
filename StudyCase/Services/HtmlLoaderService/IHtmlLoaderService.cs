using HtmlAgilityPack;

namespace StudyCase.Services.HtmlLoaderService
{
    public interface IHtmlLoaderService
    {
        Task<HtmlDocument> LoadAsync(string url);
    }
}
