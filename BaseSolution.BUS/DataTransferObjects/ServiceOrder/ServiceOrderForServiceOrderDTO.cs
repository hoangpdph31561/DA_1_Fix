using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderForServiceOrderDTO
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
