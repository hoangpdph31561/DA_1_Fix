using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request
{
    public class ViewRoomTypeWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
