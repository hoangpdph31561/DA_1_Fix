using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking
{
    public class RoomBookingDto
    {
        public Guid Id { get; set; }
        public RoomStatus StatusRoom { get; set; }
        public EntityStatus Status { get; set; }
        public BookingType BookingType { get; set; }

        // Base on
        public Guid CustomerId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public Guid RoomDetailId { get; set; }
        public string NameCustomer { get; set; }  // Tên khách hàng
        public string StaffName { get; set; } // tên nhân viên 
        public string NameBuilding { get; set; }
        public string NameFloor { get; set; }
        public string NameRoom { get; set; }
        public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public decimal ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomAmount { get; set; } // RoomAmount =UtilityExtensions.TinhTien(...);
        public decimal RoomPrice { get; set; }
        public int TotalService { get; set; } // tổng số lượng của từng dịch vụ 
        public string NameService { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PrePaid { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

        public string NameRoomType { get; set; } = string.Empty;
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
          public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public Guid RoomBookingDetailId { get; set; }

    }
}
