using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderForRoomBookingDTO
    {
        public Guid Id { get; set; }
        public Guid? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? RoomBookingDetailId { get; set; } // đặt dịch vụ cho phòng  
       public List<ServiceOrderDetailDTO>? lstServiceOrder { get; set; } 
    }
}
