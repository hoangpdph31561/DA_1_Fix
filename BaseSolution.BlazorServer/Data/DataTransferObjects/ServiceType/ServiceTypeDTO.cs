using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType
{
    public class ServiceTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        // Tổng số dịch vụ đã tạo từ type này
        public int TotalServices { get; set; }
        //Số lượng dịch vụ đã order
        public int TotalServiceOrders { get; set; }
    }
}
