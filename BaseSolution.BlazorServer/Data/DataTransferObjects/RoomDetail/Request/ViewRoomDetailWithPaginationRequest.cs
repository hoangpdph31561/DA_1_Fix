using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request
{
    public class ViewRoomDetailWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
        public Guid? BuildingId { get; set; } = Guid.Empty;
        public Guid? FloorId { get; set; } = Guid.Empty;
        public Guid? RoomTypeId { get; set; } = Guid.Empty;
    }
}
