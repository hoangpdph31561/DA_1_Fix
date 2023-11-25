using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail
{
    public class RoomDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxPeopleStay { get; set; }
        public string Description { get; set; } = string.Empty;
        public double RoomSize { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public RoomStatus Status { get; set; }
        public Guid FloorId { get; set; }
        public Guid RoomTypeId { get; set; }
        //Tên tầng 
        public string FloorName { get; set; } = string.Empty;
        //Tên loại phòng
        public string RoomTypeName { get; set; } = string.Empty;
        //ID tòa nhà
        public Guid BuildingId { get; set; }
        //Tên tòa nhà
        public string BuildingName { get; set; } = string.Empty;
    }
}
