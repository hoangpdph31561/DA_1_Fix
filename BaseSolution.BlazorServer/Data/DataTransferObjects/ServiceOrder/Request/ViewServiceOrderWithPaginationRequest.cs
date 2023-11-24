using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request
{
    public class ViewServiceOrderWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
