using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid RoomDetailId { get; set; }
        public BookingType BookingType { get; set; }
        public Guid CustomerId { get; set; }
        public EntityStatus Status { get; set; } 
    }
}
