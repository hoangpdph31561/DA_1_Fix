namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType.Request
{
    public class ServiceTypeDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
