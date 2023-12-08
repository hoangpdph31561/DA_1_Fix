using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill
{
    public class BillDtoForService
    {
        public Guid CustomerId { get; set; } // map được sẵn 
        public Guid Id { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string CustomerName { get; set; }
        public int TotalService { get; set; }
        public EntityStatus Status { get; set; }
        public decimal ServicePrice { get; set; }
        public string NameService { get; set; }
        public decimal ServiceAmount { get; set; }
    }
}
