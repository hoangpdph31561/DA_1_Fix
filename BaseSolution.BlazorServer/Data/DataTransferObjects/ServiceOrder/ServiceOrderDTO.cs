using BaseSolution.BlazorServer.Data.ValueObjects.Common;
namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Service
{
    public class ServiceOrderDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public int Quantity { get; set; } // số lượng 
        public decimal Price { get; set; }
        public float TotalAmount { get; set; }
        public EntityStatus Status { get; set; } 

        // base on 
        public string CustomerName { get; set; } = string.Empty; // Tên khách hàng
        public Guid CustomerId { get; set; } 
        public string UserName { get; set; } = string.Empty; // tên nhân viên 
        public string Name { get; set; } = string.Empty; // tên dịch vụ
        public Guid? RoomBookingDetailId { get; set; } // ncheck xem dịch vụ được đặt theo phong hay riêng 
    }
}
