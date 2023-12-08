using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request
{
    public class RoomBookingUpdateStatusRequest
    {
        public Guid Id { get; set; }
        public EntityStatus Status { get; set; }
    }
}
