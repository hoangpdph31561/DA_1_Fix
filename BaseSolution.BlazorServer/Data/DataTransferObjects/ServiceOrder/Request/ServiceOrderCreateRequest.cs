namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public decimal Price { get; set; }
    }
}
