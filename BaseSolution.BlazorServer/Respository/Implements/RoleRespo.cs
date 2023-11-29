using BaseSolution.BlazorServer.Data.DataTransferObjects.Role;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Role.Request;
using BaseSolution.BlazorServer.Data.DataTransferObjects.User;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoleRespo : IRoleRespo
    {
        private readonly HttpClient _httpClient;

        public RoleRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<RoleDto>> GetAllRole(ViewRoleWithPaginationRequest request)
        {
            try
            {
                string url = $"/api/Roles";
                var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoleDto>>(url);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
