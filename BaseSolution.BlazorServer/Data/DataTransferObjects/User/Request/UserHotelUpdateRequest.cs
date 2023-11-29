using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request
{
    public class UserHotelUpdateRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserRoleId { get; set; }
        public EntityStatus Status { get; set; }
    }
}
