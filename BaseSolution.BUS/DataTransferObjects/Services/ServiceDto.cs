using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Services
{
    public class ServiceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public Guid ServiceTypeId { get; set; }
        public EntityStatus Status { get; set; }
        public bool IsRoomBookingNeed { get; set; }
        //Tên loại dịch vụ
        public string ServiceTypeName { get; set; } = string.Empty;
        //Số lượng đã đặt
        public double  TotalOrders { get; set; }

    }
}
