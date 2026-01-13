using SchoolManager.Services.Interfaces;

namespace SchoolManager.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("University");

        }

        public async Task<T> GetDataAsync<T>(string url)
        {

            try
            {
                var response = await _httpClient.GetFromJsonAsync<T>(url);
                if (response == null)
                {

                    throw new HttpRequestException(message: "No Results found!");

                }
                ;
                return response;
            }
            catch (HttpIOException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return default;
            }
        }

    }
}
