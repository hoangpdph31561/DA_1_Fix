using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill
{
    public class BillDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? RoomBookingId { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        //Tên khách trên hóa đơn
        public string CustomerName { get; set; } = string.Empty;
        //Tên nhân viên tạo hóa đơn
        public string CreatedUserName { get; set; } = string.Empty;
    }
}
