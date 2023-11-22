using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail
{
    public class RoomBookingDetailDTO
    {
        public string NameCustomer { get; set; } = string.Empty;
        public BookingType BookingType { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public decimal PrePaid { get; set; } = 0;
    }
}
