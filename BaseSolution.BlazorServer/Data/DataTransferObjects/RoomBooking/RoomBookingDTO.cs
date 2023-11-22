using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking
{
    public class RoomBookingDto
    {
        public string NameCustomer { get; set; } = string.Empty; // Tên khách hàng
        public string UserName { get; set; } = string.Empty; // tên nhân viên 
        public string NameBuilding { get; set; } = string.Empty;
        public string NameFloor { get; set; } = string.Empty;
        public string CountServices { get; set; }
        public BookingType BookingType { get; set; }
        public float TotalAmount { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

    }
}
