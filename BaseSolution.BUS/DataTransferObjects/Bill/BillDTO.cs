using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill
{
    public class BillDTO
    {
        public Guid Id { get; set; } // mã hóa đơn
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? RoomBookingId { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public string BillType { get; set; }
        public EntityStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedUserName { get; set; }

        // Based on 
        public int TotalService { get; set; } // tổng số lượng của 1 dịch vụ 
         public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public float TotalAmount { get; set; } // TotalAmount = TotalService + RoomPrice
    }
}
