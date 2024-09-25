namespace WebAdmin.Services.Interfaces
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item);
        //Task<HttpResponseMessage> PutAsync(string uri);
        Task<HttpResponseMessage> DeleteAsync(string uri);
        //Task<HttpResponseMessage> SetToken(string token);
    }
}
