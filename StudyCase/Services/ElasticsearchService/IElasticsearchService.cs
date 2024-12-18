namespace StudyCase.Services.ElasticsearchService
{
    public interface IElasticsearchService
    {
        Task<string> SaveLinks(List<string> links);

        Task<List<string>> GetLinksFromElasticsearch(string search = null);
    }
}
