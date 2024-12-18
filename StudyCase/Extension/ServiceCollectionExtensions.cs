
using StudyCase.Configuration;

namespace StudyCase.Extension
{
    public static class ServiceCollectionExtensions
    { 
         public static void AddSozcuSettings(this IServiceCollection services, IConfiguration configuration)
         {
                // SozcuSettings sınıfını appsettings.json'dan alıyoruz
                services.Configure<SozcuSettings>(configuration.GetSection("SozcuSettings"));
          }  

    }
}
 