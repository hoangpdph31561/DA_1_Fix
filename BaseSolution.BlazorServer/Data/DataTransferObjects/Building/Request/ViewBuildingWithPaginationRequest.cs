using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Building.Request
{
    public class ViewBuildingWithPaginationRequest : PaginationRequest
    {
        public string? Search { get; set; } 
    }
}
