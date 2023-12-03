using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
