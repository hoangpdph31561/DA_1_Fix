using BaseSolution.BlazorServer.Data.DataTransferObjects.Acount;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Acount.Request;
using BaseSolution.BlazorServer.Respository.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class LoginRepo : ILogin
    {
        private readonly HttpClient _httpClient;

        public LoginRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ViewLoginInput> Login(LoginInputRequest request)
        {
            string url = $"/api/Logins";
            var response = await _httpClient.PostAsJsonAsync(url, request);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewLoginInput>(resultString);
            return result;
        }
    }
}
