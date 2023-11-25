using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request
{
    public class RoomDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxPeopleStay { get; set; }
        public string Description { get; set; } = string.Empty;
        public double RoomSize { get; set; }
        public RoomStatus Status { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Guid FloorId { get; set; }
        public Guid RoomTypeId { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
