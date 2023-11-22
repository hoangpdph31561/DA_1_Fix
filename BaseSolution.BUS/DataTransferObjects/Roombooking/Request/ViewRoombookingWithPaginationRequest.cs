using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class ViewRoombookingWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
