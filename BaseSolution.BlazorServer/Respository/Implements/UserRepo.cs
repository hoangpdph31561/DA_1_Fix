using BaseSolution.BlazorServer.Data.DataTransferObjects.User;
using BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
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

        public async Task<bool> CreateUserAsync(UserCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Users", request);

            return result.IsSuccessStatusCode;
        }
    

        public async Task<bool> DeleteUserAsync(UserDeleteRequest request)
        {
            string url = $"api/Users?Id={request.Id}";
            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<UserHotelDTO>> GetUserAsync(ViewUsersHotelWithPaginationRequest request)
        {
            string url = $"/api/Users?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if (!String.IsNullOrEmpty(request.Name))
            {
                url = $"/api/Users?Name={request.Name}&PageNumber={request.Name}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<UserHotelDTO>>(url);
            return result;
        }

        public async Task<UserHotelDTO> GetUserById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserHotelDTO>($"/api/Users/{id}");

            return result;
        }

        public async Task<bool> UpdateUserAsync(UserUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Users", request);
            return result.IsSuccessStatusCode;
        }
    }
}
