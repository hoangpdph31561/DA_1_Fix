namespace BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityRoomDetailCreateUpdateDelete
    {
        public Guid RoomTypeId { get; set; }
        public Guid AmenityId { get; set; }
        public int Amount { get; set; }
    }
}
