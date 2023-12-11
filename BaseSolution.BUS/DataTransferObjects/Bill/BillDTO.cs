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
        public Guid ServiceOrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? RoomBookingId { get; set; }
        public string CustomerName { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string BillType { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedUserName { get; set; }

        // Based on 
        public string NameService { get; set; }
        public string RoomName { get; set; }
        public int TotalService { get; set; } // tổng số lượng của 1 dịch vụ 
         public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public float TotalAmount { get; set; } // TotalAmount = TotalService + RoomPrice
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; }
    }
}
