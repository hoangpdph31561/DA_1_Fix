using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity.Request
{
    public class ViewAmenityWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
