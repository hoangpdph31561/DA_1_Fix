namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail
{
    public class ServiceOrderDetailInfo
    {
        public bool IsExist { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Amount { get; set; }
    }
}
