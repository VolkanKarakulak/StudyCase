using HtmlAgilityPack;

namespace StudyCase.Services.HtmlLoaderService
{
    public interface IHtmlLoader
    {
        HtmlDocument Load(string url);
    }
}
