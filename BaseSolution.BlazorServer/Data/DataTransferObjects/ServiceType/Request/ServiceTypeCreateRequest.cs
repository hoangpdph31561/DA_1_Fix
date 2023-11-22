namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType.Request
{
    public class ServiceTypeCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
