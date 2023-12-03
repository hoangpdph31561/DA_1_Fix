using BaseSolution.BlazorServer.Data.DataTransferObjects.Service;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Service.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IServiceRespo
    {
        Task<PaginationResponse<ServiceDTO>> GetAllServices(ViewServiceWithPaginationRequest request);
        Task<List<ServiceDTO>> GetServices(ViewServiceWithPaginationRequest request);
        Task<bool> CreateNewService(ServiceCreateRequest request);
        Task<bool> UpdateService(ServiceUpdateRequest request);
        Task<bool> DeleteService(ServiceDeleteRequest request);
        Task<ServiceDTO> GetServiceById(Guid id);
    }
}
