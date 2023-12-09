using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill.Request
{
    public class BillDtoForService
    {
        public Guid CustomerId { get; set; } // map được sẵn 
        public Guid Id { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string CustomerName { get; set; }
        public int TotalService { get; set; }
        public EntityStatus StatusServicrOrder { get; set; }
        public decimal ServicePrice { get; set; }
        public string NameService { get; set; }
        public decimal ServiceAmount { get; set; }
        public Guid? RoomBookingDetailId { get; set; }
    }
}
