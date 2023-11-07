using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrderDetail
{
    public class ServiceOrderDetailDTO
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        //Tên service
        public string ServiceName { get; set; } = string.Empty;
        //Unit service 
        public UnitType ServiceUnitType { get; set; }
    }
}
