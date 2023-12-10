using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request
{
    public class ServiceOrderCreateUpdateDeleteRequest
    {
        public Guid ServiceId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
    }
}
