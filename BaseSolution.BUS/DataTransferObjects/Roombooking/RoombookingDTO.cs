using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Roombooking
{
    public class RoombookingDTO
    {
        public Guid Id { get; set; }
        public string CodeBooking { get; set; } = string.Empty;
        public BookingType BookingType { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? UserId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
