using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class ViewAmenityWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
