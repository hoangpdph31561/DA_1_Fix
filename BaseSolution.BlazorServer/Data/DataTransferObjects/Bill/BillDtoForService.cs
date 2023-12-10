using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill
{
    public class BillDtoForService
    {
        public Guid Id { get; set; }
        public Guid ServiceOrderId { get; set; }
        public EntityStatus StatusServicerOrder { get; set; }
        public double Quantity { get; set; } // số lượng 
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public EntityStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        // base on 
        public string CustomerName { get; set; }  // Tên khách hàng
        public Guid CustomerId { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid? RoomBookingDetailId { get; set; } // đặt dịch vụ cho phòng  
    }
}
