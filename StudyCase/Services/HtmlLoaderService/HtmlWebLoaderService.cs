using HtmlAgilityPack;

namespace StudyCase.Services.HtmlLoaderService
{
    public class HtmlWebLoaderService : IHtmlLoaderService
    {
        public HtmlDocument Load(string url)
        {
            var web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
