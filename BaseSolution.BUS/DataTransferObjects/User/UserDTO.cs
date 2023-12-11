using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public Guid UserRoleId { get; set; }
        public string RoleCode { get; set; }
        public EntityStatus Status { get; set; } 
    }
}
