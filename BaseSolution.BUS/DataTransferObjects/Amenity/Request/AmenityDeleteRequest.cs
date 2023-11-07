using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class AmenityDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
