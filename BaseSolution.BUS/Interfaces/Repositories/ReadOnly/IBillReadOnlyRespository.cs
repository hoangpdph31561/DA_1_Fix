using BaseSolution.Application.DataTransferObjects.Bill;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IBillReadOnlyRespository
    {
        Task<RequestResult<BillDTO?>> GetBillByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<RequestResult<BillDtoForRoom?>> GetBillByIdForRoomAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<BillDtoForService?>> GetBillByIdForServiceAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<List<BillDTO?>>> GetBillByIdCustomerAsync(Guid idCustomer, CancellationToken cancellationToken);

        Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByAdminAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByOtherAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BillDtoForRoom>>> GetBillsByOtherForRoom(ViewBillWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BillDtoForService>>> GetBillsByOtherForService(ViewBillWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
