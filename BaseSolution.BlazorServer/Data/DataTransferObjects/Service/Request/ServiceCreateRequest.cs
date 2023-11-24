namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Service.Request
{
    public class ServiceCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public Guid ServiceTypeId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
