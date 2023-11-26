using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Roombooking
{
    public class RoombookingDTO
    {
        public Guid Id { get; set; }
        public EntityStatus Status { get; set; } 
        public BookingType BookingType { get; set; }

        // Base on
        public Guid CustomerId { get; set; }
        public Guid RoomDetailId { get; set; }
        public string NameCustomer { get; set; }  // Tên khách hàng
        public string StaffName { get; set; } // tên nhân viên 
        public string NameBuilding { get; set; } 
        public string NameFloor { get; set; } 
        public string NameRoom { get; set; } 
        public int CountServices { get; set; } // tổng số lượng của 1 dịch vụ 
        public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public int TotalService { get; set; }
        public string NameService { get; set; }
        public float TotalAmount { get; set; }
        public decimal PrePaid { get; set; } = 0;
        public DateTimeOffset CreatedTime { get; set; }
        public string NameRoomType { get; set; } = string.Empty;
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
    }
}
