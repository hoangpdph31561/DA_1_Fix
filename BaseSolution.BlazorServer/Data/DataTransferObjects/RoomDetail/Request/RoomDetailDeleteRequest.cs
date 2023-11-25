namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request
{
    public class RoomDetailDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
