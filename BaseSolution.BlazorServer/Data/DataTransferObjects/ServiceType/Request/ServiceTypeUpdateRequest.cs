using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceType.Request
{
    public class ServiceTypeUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
