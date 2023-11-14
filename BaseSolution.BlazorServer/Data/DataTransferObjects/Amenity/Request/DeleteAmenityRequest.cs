namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity.Request
{
    public class DeleteAmenityRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
