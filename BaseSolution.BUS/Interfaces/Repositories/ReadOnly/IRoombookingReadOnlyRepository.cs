using BaseSolution.Application.DataTransferObjects.Roombooking;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoombookingReadOnlyRepository
    {
        Task<RequestResult<RoombookingDTO?>> GetRoombookingByIdAsync(Guid idRoombooking, CancellationToken cancellationToken);
        Task<RequestResult<List<RoombookingDTO?>>> GetRoombookingByIdCustomerAsync(Guid idCustomer, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoombookingDTO>>> GetRoombookingWithPaginationByAdminAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoombookingDTO>>> GetRoombookingWithPaginationByOtherAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoombookingDTO>>> GetRoombookingWithPaginationByAwaitAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
