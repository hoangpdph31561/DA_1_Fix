using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceType
{
    public class ServiceTypeDto
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
