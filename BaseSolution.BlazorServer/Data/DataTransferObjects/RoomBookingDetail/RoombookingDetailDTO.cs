using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail
{
    public class RoomBookingDetailDTO
    {
        public Guid Id { get; set; }
        public string NameCustomer { get; set; } = string.Empty;
        public BookingType BookingType { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; } = 0;
        public decimal Price { get; set; }
        public Guid RoomDetailId { get; set; }
        public Guid RoomBookingId { get; set; }
        public int CountRoom {  get; set; }
    }
}
