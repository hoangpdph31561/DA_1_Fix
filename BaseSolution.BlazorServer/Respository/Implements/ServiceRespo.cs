using BaseSolution.BlazorServer.Data.DataTransferObjects.Service;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Service.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class ServiceRespo : IServiceRespo
    {
        private readonly HttpClient _httpClient;
        public ServiceRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewService(ServiceCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Services", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteService(ServiceDeleteRequest request)
        {
            string url = $"/api/Services?Id={request.Id}";
            if(!string.IsNullOrWhiteSpace(request.DeletedBy.ToString()))
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<ServiceDTO>> GetAllServices(ViewServiceWithPaginationRequest request)
        {
            string url = $"/api/Services?ServiceTypeId={request.ServiceTypeId}";
            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                url += $"&SearchString={request.SearchString}";
            }
            url += $"&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<ServiceDTO>>(url);
            return result;
        }

        public async Task<ServiceDTO> GetServiceById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceDTO>($"/api/Services/{id}");
            return result;
        }

        public async Task<List<ServiceDTO>> GetServices(ViewServiceWithPaginationRequest request)
        {
            string url = $"/api/Services/getServiceAsync";
            var result = await _httpClient.GetFromJsonAsync<List<ServiceDTO>>(url);
            return result;
        }

        public async Task<bool> UpdateService(ServiceUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Services", request);
            return result.IsSuccessStatusCode;
        }
    }
}
