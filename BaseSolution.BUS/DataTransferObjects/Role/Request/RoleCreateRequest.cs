using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string RoleCode { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
