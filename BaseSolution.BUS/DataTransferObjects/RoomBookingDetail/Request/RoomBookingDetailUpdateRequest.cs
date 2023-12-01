using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
       
    }
}
