using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.RoomBooking.Request
{
    public class RoomBookingUpdateStatusRequest
    {
        public Guid Id { get; set; }
        public EntityStatus Status { get; set; }
    }
}
