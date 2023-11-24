using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request
{
    public class ViewCustomerBillWithPaginationRequest : PaginationRequest
    {
        public string? Search {  get; set; }
    }
}
