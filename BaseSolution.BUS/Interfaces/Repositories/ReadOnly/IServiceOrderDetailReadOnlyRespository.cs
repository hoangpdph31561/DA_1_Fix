using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IServiceOrderDetailReadOnlyRespository
    {
        Task<RequestResult<ServiceOrderDetailDTO?>> GetServiceOrderDetailByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ServiceOrderDetailDTO>>> GetServiceOrderDetailsByAdminAsync(ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ServiceOrderDetailDTO>>> GetServiceOrderDetailsByOtherAsync(ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<ServiceOrderDetailDTO>>> GetServiceOrderDetailsByServiceOrderAsync(ViewServiceOrderDetailByIdServiceOderRequest request, CancellationToken cancellationToken);

    }
}
