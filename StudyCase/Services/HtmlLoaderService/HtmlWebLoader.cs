using HtmlAgilityPack;

namespace StudyCase.Services.HtmlLoaderService
{
    public class HtmlWebLoader : IHtmlLoader
    {
        public HtmlDocument Load(string url)
        {
            var web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
