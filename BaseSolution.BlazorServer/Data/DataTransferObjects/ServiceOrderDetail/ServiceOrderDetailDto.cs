namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail
{
    public class ServiceOrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public decimal Price { get; set; }
        public double Amount { get; set; }
        //Tên service
        public string ServiceName { get; set; } 
        //Unit service 
        public string ServiceUnitType { get; set; } 
    }
}
