using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request
{
    public class ViewRoomBookingDetailWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
