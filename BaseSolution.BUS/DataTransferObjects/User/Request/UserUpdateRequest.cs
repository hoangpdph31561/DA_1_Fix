using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public Guid UserRoleId { get; set; }
        public EntityStatus Status { get; set; } 
    }
}
