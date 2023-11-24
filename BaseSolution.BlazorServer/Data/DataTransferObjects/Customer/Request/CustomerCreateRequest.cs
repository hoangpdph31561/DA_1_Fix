using BaseSolution.BlazorServer.Data.ValueObjects.Common;
namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ApprovedCode { get; set; }
        public DateTimeOffset? ApprovedCodeExpiredTime { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Locked;
    }
}
