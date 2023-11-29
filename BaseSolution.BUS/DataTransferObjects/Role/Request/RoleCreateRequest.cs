using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleCreateRequest
    {
        public string Name { get; set; } 
        public string RoleCode { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
