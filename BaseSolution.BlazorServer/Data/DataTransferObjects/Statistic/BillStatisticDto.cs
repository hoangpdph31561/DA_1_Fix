namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Statistic
{
    public class BillStatisticDto
    {
        public int Month { get; set; }
        public int TotalService { get; set; } // tổng số lượng của 1 dịch vụ 
        public decimal ServicePrice { get; set; } // giá của dịch vụ 
        public float ServiceAmount { get; set; }  // ServiceAmount = TotalService x ServicePrice
        public decimal RoomPrice { get; set; }
        public int TotalRoom { get; set; }
        public float RoomAmount { get; set; }
        public float TotalAmount { get; set; }
        public int TotalAmountForMonth { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
