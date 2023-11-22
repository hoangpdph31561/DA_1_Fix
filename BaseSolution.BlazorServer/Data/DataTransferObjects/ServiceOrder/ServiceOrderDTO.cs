using BaseSolution.BlazorServer.Data.ValueObjects.Common;
namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Service
{
    public class ServiceOrderDTO
    {
        public string CustomerName { get; set; } = string.Empty; // Tên khách hàng
        public string UserName { get; set; } = string.Empty; // tên nhân viên 
        public string Name { get; set; } = string.Empty; // tên dịch vụ
        public DateTimeOffset CreatedTime { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public float TotalAmount { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
