using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid RoomDetailId { get; set; }
        public BookingType BookingType { get; set; }
        public string CodeBooking { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
    }
}
