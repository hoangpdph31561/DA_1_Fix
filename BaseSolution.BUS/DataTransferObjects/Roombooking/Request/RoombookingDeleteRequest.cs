namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
