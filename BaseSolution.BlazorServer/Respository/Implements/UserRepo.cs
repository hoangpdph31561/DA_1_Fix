using BaseSolution.BlazorServer.Respository.Interfaces;


namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class UserRepo : IUserRepo
    {
        private readonly HttpClient _httpClient;
        public UserRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ConfirmAccountAsync(string username, string password)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/Users/confirmAccount/{username}/{password}", "");
            var convert = result.Content.ReadAsStringAsync();
            return result.IsSuccessStatusCode;
        }
    }
}
