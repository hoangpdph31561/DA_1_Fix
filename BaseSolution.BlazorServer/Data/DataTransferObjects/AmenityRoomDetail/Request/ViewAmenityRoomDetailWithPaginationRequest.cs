using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request
{
    public class ViewAmenityRoomDetailWithPaginationRequest : PaginationRequest
    {
        public Guid RoomTypeId { get; set; } = Guid.Empty;
    }
}
