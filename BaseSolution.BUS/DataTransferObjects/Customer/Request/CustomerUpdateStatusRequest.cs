using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Customer.Request
{
    public class CustomerUpdateStatusRequest
    {
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
