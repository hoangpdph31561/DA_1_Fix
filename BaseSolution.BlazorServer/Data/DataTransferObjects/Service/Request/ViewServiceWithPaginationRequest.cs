using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Service.Request
{
    public class ViewServiceWithPaginationRequest : PaginationRequest
    {
        public Guid ServiceTypeId { get; set; }
        public string? SearchString { get; set; }
    }
}
