using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;
namespace BaseSolution.Domain.Entities
{
    public class RoomBookingEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string CodeBooking { get; set; } = string.Empty;
        public BookingType BookingType { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? UserId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public CustomerEntity Customer { get; set; }
        public UserEntity User { get; set; }
        public List<RoomBookingDetailEntity> RoomBookingDetails { get; set; }
    }
}
