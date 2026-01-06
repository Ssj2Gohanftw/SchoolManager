using SchoolManager.Services.Interfaces;
using System.Text;

namespace SchoolManager.Services
{
    public class ApiService:IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("University");

        }

        public async Task<T> GetDataAsync<T>(string url)
        {
            var res = await _httpClient.GetFromJsonAsync<T>(url);
            if (res == null) throw new InvalidOperationException();

            return res;
        }

        

    }
}
