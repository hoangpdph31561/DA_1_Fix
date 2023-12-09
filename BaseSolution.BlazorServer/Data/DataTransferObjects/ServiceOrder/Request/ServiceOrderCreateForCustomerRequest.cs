namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderCreateForCustomerRequest
    {
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
