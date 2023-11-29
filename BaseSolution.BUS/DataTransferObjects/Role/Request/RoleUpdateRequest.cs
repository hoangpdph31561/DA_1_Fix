using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleUpdateRequest
    {
        public Guid Id { get; set; }
        public string RoleCode { get; set; } = string.Empty;
       
    }
}
