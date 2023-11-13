namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request
{
    public class FloorCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
