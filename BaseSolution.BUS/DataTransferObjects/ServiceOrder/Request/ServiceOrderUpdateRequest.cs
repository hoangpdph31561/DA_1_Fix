using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public EntityStatus Status { get; set; }
    }
}
