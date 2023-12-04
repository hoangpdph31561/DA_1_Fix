using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public EntityStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
