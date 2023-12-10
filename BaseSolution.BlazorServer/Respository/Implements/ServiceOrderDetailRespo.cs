using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class ServiceOrderDetailRespo : IServiceOrderDetailRespo
    {
        private readonly HttpClient _httpClient;

        public ServiceOrderDetailRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateUpdateDeleteServiceOrder(List<ServiceOrderDetailCreateUpdateDelete> request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/ServiceOrderDetails/createUpdateDeleteServiceOrderDetail", request);
            return result.IsSuccessStatusCode; throw new NotImplementedException();
        }

        public async Task<List<ServiceOrderDetailDto>> GetServiceOrderDetailByServiceOrderId(ViewServiceOrderDetailByIdServiceOderRequest request)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ServiceOrderDetailDto>>($"/api/ServiceOrderDetails/getServiceOrderDetailByIdServiceOrder?idServiceOrder={request.ServiceOrderId}");
            return result;
        }
    }
}
