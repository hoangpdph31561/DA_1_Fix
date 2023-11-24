using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class ViewCustomerWithPaginationRequest : PaginationRequest
    {
        public string? Name {  get; set; }
    }
}
