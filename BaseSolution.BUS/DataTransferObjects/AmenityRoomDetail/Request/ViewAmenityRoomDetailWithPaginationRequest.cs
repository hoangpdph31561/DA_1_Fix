using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class ViewAmenityRoomDetailWithPaginationRequest : PaginationRequest
    {
        public Guid RoomTypeId { get; set; } = Guid.Empty;
    }
}
