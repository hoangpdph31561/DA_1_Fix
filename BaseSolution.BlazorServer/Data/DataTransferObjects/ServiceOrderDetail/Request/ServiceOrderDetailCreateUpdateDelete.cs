namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail.Request
{
    public class ServiceOrderDetailCreateUpdateDelete
    {
        public Guid ServiceId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
    }
}
