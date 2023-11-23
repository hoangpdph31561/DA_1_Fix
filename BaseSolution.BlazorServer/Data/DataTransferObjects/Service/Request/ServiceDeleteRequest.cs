namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Service.Request
{
    public class ServiceDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
