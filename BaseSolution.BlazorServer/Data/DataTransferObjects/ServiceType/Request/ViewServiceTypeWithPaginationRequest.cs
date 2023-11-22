using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType.Request
{
    public class ViewServiceTypeWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
