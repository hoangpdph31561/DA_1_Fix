using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class AmenityCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
