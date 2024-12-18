using HtmlAgilityPack;

namespace StudyCase.Services.HtmlLoaderService
{
    public interface IHtmlLoaderService
    {
        HtmlDocument Load(string url);
    }
}
