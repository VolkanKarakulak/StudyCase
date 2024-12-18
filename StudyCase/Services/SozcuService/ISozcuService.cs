namespace StudyCase.Services.SozcuService
{
    public interface ISozcuService
    {
        Task<List<string>> GetLinksFromSozcu();
    }
}
