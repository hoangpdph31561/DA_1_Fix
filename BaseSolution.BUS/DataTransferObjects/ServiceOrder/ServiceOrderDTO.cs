using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderDTO
    {
        public DateTimeOffset CreatedTime { get; set; }
        public int Quantity { get; set; } // số lượng 
        public decimal Price { get; set; }
        public float TotalAmount { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        // base on 
        public string CustomerName { get; set; } = string.Empty; // Tên khách hàng
        public string UserName { get; set; } = string.Empty; // tên nhân viên 
        public string Name { get; set; } = string.Empty; // tên dịch vụ
        public Guid? RoomBookingDetailId { get; set; } // ncheck xem dịch vụ được đặt theo phong hay riêng 
    }
}
