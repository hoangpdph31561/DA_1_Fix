using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IBillRespo
    {
        Task<PaginationResponse<BillDTO>> GetAllBill(ViewBillWithPaginationRequest request);
        Task<bool> CreateNewBill(BillCreateRequest request);
    }
}
