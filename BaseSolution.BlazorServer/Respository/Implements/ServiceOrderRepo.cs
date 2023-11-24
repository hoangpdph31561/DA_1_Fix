using BaseSolution.BlazorServer.Data.DataTransferObjects.Service;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class ServiceOrderRepo : IServiceOrderRepo
    {
        private readonly HttpClient _httpClient;

        public ServiceOrderRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<ServiceOrderDTO>> GetAllServices(ViewServiceOrderWithPaginationRequest request)
        {
            string url = $"/api/ServiceOrders/serviceOrdersByOther?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                url = $"/api/ServiceOrders/serviceOrdersByOther?SearchString={request.SearchString}&?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<ServiceOrderDTO>>(url);
            return result;
        }
    }
}
