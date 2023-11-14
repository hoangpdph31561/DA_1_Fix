using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor
{
    public class FloorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public EntityStatus Status { get; set; }
        //Tên tòa nhà chứa tầng
        public string BuildingName { get; set; } = string.Empty;
        //Số phòng được tạo để thuê trên tầng
        public int NumberOfRoomRent { get; set; }
    }
}
