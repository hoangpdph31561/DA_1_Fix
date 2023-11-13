using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request
{
    public class ViewFloorWithPaginationRequest : PaginationRequest
    {
        public Guid BuildingId { get; set; }
        public string? SearchString { get; set; } 
    }
}
