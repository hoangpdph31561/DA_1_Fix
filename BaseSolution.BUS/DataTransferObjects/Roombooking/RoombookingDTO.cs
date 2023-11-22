using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Roombooking
{
    public class RoombookingDTO
    {
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public BookingType BookingType { get; set; }

        // Base on
        public string NameCustomer { get; set; } = string.Empty; // Tên khách hàng
        public string StaffName { get; set; } = string.Empty; // tên nhân viên 
        public string NameBuilding { get; set; } = string.Empty;
        public string NameFloor { get; set; } = string.Empty;
        public string NameRoom { get; set; } = string.Empty;
        public int CountServices { get; set; } // tổng số lượng của 1 dịch vụ 
        public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public float TotalAmount { get; set; }
  
    }
}
