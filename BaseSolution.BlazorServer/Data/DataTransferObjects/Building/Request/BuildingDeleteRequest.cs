namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Building.Request
{
    public class BuildingDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
