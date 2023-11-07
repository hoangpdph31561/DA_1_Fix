using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Amenity
{
    public class AmenityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
       
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
