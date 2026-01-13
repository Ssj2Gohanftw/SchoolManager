namespace SchoolManager.Services.Interfaces
{
    public interface IApiService
    {
        public Task<T> GetDataAsync<T>(string url);


    }
}
