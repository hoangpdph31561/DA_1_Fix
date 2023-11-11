using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Building
{
    public class BuildingDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberOfFloors { get; set; }
        public EntityStatus Status { get; set; }
    }
}
