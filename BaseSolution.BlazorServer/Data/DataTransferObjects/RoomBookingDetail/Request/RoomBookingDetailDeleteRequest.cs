namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
