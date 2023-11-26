using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail
{
    public class RoombookingDetailDTO
    {
        public string NameCustomer { get; set; } = string.Empty;
        public BookingType BookingType { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; } = 0;
        public Guid RoomDetailId { get; set; }
        public Guid RoomBookingId { get; set; }
        public int CountRoom {  get; set; }
    }
}
