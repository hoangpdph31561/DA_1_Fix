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
        public Guid? RoomBookingDetailId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
