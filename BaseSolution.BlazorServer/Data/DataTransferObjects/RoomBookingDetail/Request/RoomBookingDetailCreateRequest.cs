namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailCreateRequest
    {
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
    }
}
