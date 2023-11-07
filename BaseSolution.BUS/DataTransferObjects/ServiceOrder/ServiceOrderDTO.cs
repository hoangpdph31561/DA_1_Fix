using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderDTO
    {
        public Guid Id { get; set; }
        public Guid? RoomBookingDetailId { get; set; }
        public Guid? CustomerId { get; set; }
        //Tên khách hàng
        public string CustomerName { get; set; } = string.Empty;
    }
}
