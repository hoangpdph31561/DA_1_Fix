using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill
{
    public class BillDtoForRoom
    {
        public Guid CustomerId { get; set; } // map được sẵn 
        public Guid Id { get; set; }
        public Guid ServiceOrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? RoomBookingId { get; set; } // // map được sẵn 
        public string RoomName { get; set; }
        public RoomStatus StatusRoom { get; set; }
        public int TotalService { get; set; }
        public decimal ServicePrice { get; set; }
        public string NameService { get; set; }
        public decimal ServiceAmount { get; set; } // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public decimal RoomAmount { get; set; }
        public decimal TotalAmount { get; set; } // TotalAmount = TotalService + RoomPrice
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; }

    }
}
