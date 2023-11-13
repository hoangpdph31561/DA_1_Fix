namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request
{
    public class FloorDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
