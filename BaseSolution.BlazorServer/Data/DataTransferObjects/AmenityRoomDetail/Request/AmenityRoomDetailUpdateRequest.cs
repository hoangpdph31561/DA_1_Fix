using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityRoomDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public string NameOfAmenity { get; set; }
        public Guid RoomTypeId { get; set; }
        public int Amount { get; set; }
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
