using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IBillRepo
    {
        Task<PaginationResponse<BillDTO>> GetAllBill(ViewCustomerBillWithPaginationRequest request);

    }
}
