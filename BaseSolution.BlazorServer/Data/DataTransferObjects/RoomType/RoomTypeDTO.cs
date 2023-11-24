using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType
{
    public class RoomTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfAmenityRoomDetail { get; set; }
        public int AmountOfRoomType { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
