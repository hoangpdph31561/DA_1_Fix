using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill
{
    public class BillDTO
    {
        public Guid Id { get; set; } // mã hóa đơn
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string BillType { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedUserName { get; set; }
        public EntityStatus Status { get; set; }

        // Based on 
        public int TotalService { get; set; } // tổng số lượng của 1 dịch vụ 
        public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public float RoomPrice { get; set; }
        public float TotalAmount { get; set; } // TotalAmount = TotalService + RoomPrice

    }
}
