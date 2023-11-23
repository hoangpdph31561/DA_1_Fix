using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.DataTransferObjects.RoomType.Request
{
    public class ViewRoomTypeWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
