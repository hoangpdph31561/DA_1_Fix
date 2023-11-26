using BaseSolution.BlazorServer.Data.DataTransferObjects.Service;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IServiceOrderRepo
    {
        Task<PaginationResponse<ServiceOrderDTO>> GetAllServices(ViewServiceOrderWithPaginationRequest request);
        Task<bool> CreateNewService(ServiceOrderCreateRequest request);
        Task<List<ServiceOrderDTO>> GetRoomDetailByIdCustomer(Guid id);
        Task<ServiceOrderDTO> GetServiceOrderById(Guid id);
        Task<bool> UpdateServiceOrder(ServiceOrderUpdateRequest request);
    }
}
