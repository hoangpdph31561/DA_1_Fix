using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking
{
    public class RoomBookingDto
    {
        public Guid Id { get; set; }
        public EntityStatus Status { get; set; } 
        public BookingType BookingType { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string NameRoomType { get; set; }
        public decimal PrePaid { get; set; }
        // Base on
        public Guid CustomerId { get; set; }
        public Guid RoomDetailId { get; set; }
        public string NameCustomer { get; set; } // Tên khách hàng
        public string StaffName { get; set; } // tên nhân viên 
        public string NameBuilding { get; set; }
        public string NameFloor { get; set; } 
        public string NameRoom { get; set; } 
        public int CountServices { get; set; } // tổng số lượng của 1 dịch vụ 
        public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public float TotalAmount { get; set; }


    }
}
