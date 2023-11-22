using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class ServiceTypeRespo : IServiceTypeRespo
    {
        private readonly HttpClient _httpClient;
        public ServiceTypeRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateNewServiceType(ServiceTypeCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/ServiceTypes",request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteServiceType(ServiceTypeDeleteRequest request)
        {
            string url = $"api/ServiceTypes?Id={request.Id}";
            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<ServiceTypeDTO>> GetAllServiceTypes(ViewServiceTypeWithPaginationRequest request)
        {
            string url = $"/api/ServiceTypes?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if (!String.IsNullOrWhiteSpace(request.SearchString))
            {
                url = $"/api/ServiceTypes?SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<ServiceTypeDTO>>(url);
            return result;
        }

        public async Task<ServiceTypeDTO> GetServiceTypeById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceTypeDTO>($"/api/ServiceTypes/{id}");
            return result;
        }

        public async Task<bool> UpdateServiceType(ServiceTypeUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/ServiceTypes", request);
            return result.IsSuccessStatusCode;
        }
    }
}
