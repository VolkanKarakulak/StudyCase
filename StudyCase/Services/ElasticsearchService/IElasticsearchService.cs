namespace StudyCase.Services.ElasticsearchService
{
    public interface IElasticsearchService
    {
        string SaveLinks(List<string> links);

        List<string> GetLinksFromElasticsearch(string search = null);
    }
}
