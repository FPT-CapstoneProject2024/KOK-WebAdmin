using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Services.Implementation
{
    public class ApiClient : IApiClient
    {
        private const string JwtAccessToken = "jwtAccessToken";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateHttpClient()
        {
            var client = httpClientFactory.CreateClient();
            var jwtAccessToken = httpContextAccessor.HttpContext!.Request.Cookies[JwtAccessToken];

            if (!string.IsNullOrEmpty(jwtAccessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtAccessToken);
            }

            return client;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var client = CreateHttpClient();
            return await client.DeleteAsync(uri);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var client = CreateHttpClient();
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            var client = CreateHttpClient();
            var jsonData = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return await client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item)
        {
            var client = CreateHttpClient();
            return await client.PutAsJsonAsync(uri, item);
        }
    }
}
