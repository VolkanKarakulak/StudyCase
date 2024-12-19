using HtmlAgilityPack;
using System;
using static System.Net.WebRequestMethods;

namespace StudyCase.Services.HtmlLoaderService
{
    // Servisin amacı bir URL'den HTML içeriği almayı sağlamaktır.
    public class HtmlLoaderService : IHtmlLoaderService
    {
        // HttpClient, HTTP isteklerini göndermek için kullanılan bir sınıftır.
        private readonly HttpClient _httpClient;

        public HtmlLoaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // LoadAsync metodu, belirtilen URL'den HTML dokümanını asenkron olarak yükler.
        public async Task<HtmlDocument> LoadAsync(string url)
        {
            // _httpClient.GetStringAsync metodu, verilen URL'ye HTTP GET isteği yapar.
            // Yanıt gövdesini (response body) bir string olarak döndürür.
            var response = await _httpClient.GetStringAsync(url);

            // HtmlDocument, HTML içeriğini işlemek ve analiz(parse) etmek için kullanılan bir sınıftır.
            var document = new HtmlDocument();

            // LoadHtml metodu, string formatındaki HTML içeriğini HtmlDocument nesnesine yükler.
            document.LoadHtml(response);
            return document;
        }
    }
}
