using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class AmenityDeleteRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? DeletedBy { get; set; }
    }
}
