using Microsoft.Extensions.Configuration;
using StudyCase.Configuration;
using StudyCase.Extension;
using StudyCase.Services.ElasticsearchService;
using StudyCase.Services.HtmlLoaderService;
using StudyCase.Services.LinkProcessingService;
using StudyCase.Services.SozcuService;
using StudyCase.Services.WebCrawlerService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SozcuSettings>(builder.Configuration.GetSection("SozcuSettings"));
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IWebCrawlerService, WebCrawlerService>();  // WebCrawlerService'i DI'ye ekliyoruz.
builder.Services.AddScoped<IElasticsearchService, ElasticsearchService>();  
builder.Services.AddScoped<ILinkProcessingService, LinkProcessingService>();
builder.Services.AddScoped<ISozcuService, SozcuService>();
builder.Services.AddScoped<IHtmlLoaderService, HtmlLoaderService>();  // HtmlLoader'ý DI'ye ekliyoruz (gerekliyse)



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
