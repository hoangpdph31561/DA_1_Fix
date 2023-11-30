using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request
{
    public class RoomDetailUpdateStatusRequest
    {
        public Guid Id { get; set; }
        public RoomStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
