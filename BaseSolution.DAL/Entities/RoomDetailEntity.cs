using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;

namespace BaseSolution.Domain.Entities
{
    public class RoomDetailEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxPeopleStay { get; set; }
        public string Description { get; set; } = string.Empty;
        public double RoomSize { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Guid FloorId { get; set; }
        public Guid RoomTypeId { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Vacant;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public FloorEntity Floor { get; set; }
        public RoomTypeEntity RoomType { get; set; }
        public List<RoomBookingDetailEntity> RoomBookingDetails { get; set; }
    }
}
