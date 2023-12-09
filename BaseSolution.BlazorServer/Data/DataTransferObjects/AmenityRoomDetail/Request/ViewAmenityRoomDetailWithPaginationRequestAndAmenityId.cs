using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request
{
    public class ViewAmenityRoomDetailWithPaginationRequestAndAmenityId : PaginationRequest
    {
        public Guid AmenityId { get; set; } = Guid.Empty;
    }
}
