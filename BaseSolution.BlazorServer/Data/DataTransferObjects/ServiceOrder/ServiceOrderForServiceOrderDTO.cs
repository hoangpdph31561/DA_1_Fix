namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderForServiceOrderDTO
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
