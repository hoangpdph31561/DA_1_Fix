using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail
{
    public class RoomBookingDetailDTO
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; } = 0;
        public Guid RoomDetailId { get; set; }
        public Guid RoomBookingId { get; set; }
    }
}
