using BaseSolution.BlazorServer.Data.DataTransferObjects.Service;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IServiceOrderRepo
    {
        Task<PaginationResponse<ServiceOrderDTO>> GetAllServices(ViewServiceOrderWithPaginationRequest request);
        Task<bool> CreateNewService(ServiceOrderCreateRequest request);
        Task<bool> CreateServiceForRoomBooking(ServiceOrderCreateForRoomBookingRequest request);
        Task<List<ServiceOrderForRoomBookingDTO>> GetServiceOrderByIdRoomBooking(Guid id);
        Task<ServiceOrderDTO> GetServiceOrderById(Guid id);
        Task<bool> UpdateServiceOrder(ServiceOrderUpdateRequest request);
    }
}
