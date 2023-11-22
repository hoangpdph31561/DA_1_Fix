using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IServiceTypeRespo
    {
        Task<PaginationResponse<ServiceTypeDTO>> GetAllServiceTypes(ViewServiceTypeWithPaginationRequest request);
        Task<bool> CreateNewServiceType(ServiceTypeCreateRequest request);
        Task<bool> UpdateServiceType(ServiceTypeUpdateRequest request);
        Task<bool> DeleteServiceType(ServiceTypeDeleteRequest request);
        Task<ServiceTypeDTO> GetServiceTypeById(Guid id);
    }
}
