using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request
{
    public class ServiceOrderDetailCreateRequest
    {
        public Guid ServiceId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public decimal Price { get; set; }
        public double Amount { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
