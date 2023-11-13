using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request
{
    public class FloorUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
