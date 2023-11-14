namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity.Request
{
    public class CreateAmenityRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
