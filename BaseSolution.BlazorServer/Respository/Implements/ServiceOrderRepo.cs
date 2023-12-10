using BaseSolution.BlazorServer.Data.DataTransferObjects.Service;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using System;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class ServiceOrderRepo : IServiceOrderRepo
    {
        private readonly HttpClient _httpClient;

        public ServiceOrderRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewService(ServiceOrderCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/ServiceOrders", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> CreateServiceForRoomBooking(ServiceOrderCreateForRoomBookingRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/ServiceOrders/CreateNewServiceOrderForRoomBooking", request);
            return result.IsSuccessStatusCode;
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

        public async Task<List<ServiceOrderForRoomBookingDTO>> GetServiceOrderByIdRoomBooking(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ServiceOrderForRoomBookingDTO>>($"api/ServiceOrders/ServiceOrdersByIdRoomBooking?idRoombooking={id}");
            return result;
        }

        public async Task<ServiceOrderDTO> GetServiceOrderById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceOrderDTO>($"/api/ServiceOrders/{id}");
            return result;
        }

        public async Task<bool> UpdateServiceOrder(ServiceOrderUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/ServiceOrders", request);
            return result.IsSuccessStatusCode;
        }

    }
}