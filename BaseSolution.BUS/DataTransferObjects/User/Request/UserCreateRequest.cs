using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.User.Request
{
    public class UserCreateRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid UserRoleId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? CreatedBy { get; set; }
    }
}
