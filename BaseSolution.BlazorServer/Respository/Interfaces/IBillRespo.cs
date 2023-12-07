using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IBillRespo
    {
        Task<PaginationResponse<BillDTO>> GetAllBill(ViewBillWithPaginationRequest request);

        Task<BillDtoForService> GetBillByIdForService(Guid id);
        Task<BillDtoForRoom> GetBillByIdForRoom(Guid id);
        Task<PaginationResponse<BillDtoForService>> GetBillForService(ViewBillWithPaginationRequest request);
        Task<PaginationResponse<BillDtoForRoom>> GetBillForRoom(ViewBillWithPaginationRequest request);

        Task<List<BillDTO>> GetBillByIdCustomer(Guid idCustomer);

        Task<bool> CreateNewBill(BillCreateRequest request);
        Task<BillDTO> GetBillById(Guid id);
        Task<bool> UpdateBill(BillUpdateRequest request);
    }
}
