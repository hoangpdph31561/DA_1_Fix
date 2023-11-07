using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Role
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RoleCode { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
