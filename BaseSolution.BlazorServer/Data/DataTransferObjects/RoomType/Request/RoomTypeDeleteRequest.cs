namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request
{
    public class RoomTypeDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
