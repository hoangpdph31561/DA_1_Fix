using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderCreateForRoomBookingRequest
    {
        public Guid RoomBookingDetailId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Price { get; set; }
    }
}
