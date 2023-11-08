using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingCreateRequest
    {
        public string CodeBooking { get; set; } = string.Empty;
        public BookingType BookingType { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
