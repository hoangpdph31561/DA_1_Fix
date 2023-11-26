using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request
{
    public class BillUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public EntityStatus Status { get; set; }
    }
}
