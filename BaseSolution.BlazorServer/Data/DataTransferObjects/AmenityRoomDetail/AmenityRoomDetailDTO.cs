using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail
{
    public class AmenityRoomDetailDTO
    {
        public Guid Id { get; set; }
        public string NameOfAmenity { get; set; }
        public Guid AmenityId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int Amount { get; set; }
        public EntityStatus Status { get; set; } 
    }
}
