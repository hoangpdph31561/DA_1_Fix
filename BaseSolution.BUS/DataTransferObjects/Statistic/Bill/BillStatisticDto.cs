using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Statistic.Bill
{
    public class BillStatisticDto
    {
        public int Month { get; set; }
        public int TotalServiceForRoom { get; set; } // tổng số lượng của 1 dịch vụ 
        public decimal ServicePriceForRoom { get; set; }
        public string NameServiceForRoom { get; set; }
        public decimal ServiceAmountForRoom { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public decimal RoomAmount { get; set; }
        public decimal TotalAmount { get; set; } // TotalAmount = TotalService + RoomPrice
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public double QuantityService { get; set; } // số lượng 
        public decimal PriceService { get; set; }
        public decimal TotalPriceForService { get; set; } // tổng tiền của từng cái 
        public decimal TotalAmountForService { get; set; }

        public decimal TotalAll { get;set; }



    }
}
