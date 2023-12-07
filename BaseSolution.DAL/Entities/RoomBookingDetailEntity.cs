using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;


namespace BaseSolution.Domain.Entities
{
    public class RoomBookingDetailEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; } = 0;
        public Guid RoomDetailId { get; set; }
        public Guid RoomBookingId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public RoomDetailEntity RoomDetail { get; set; }
        public RoomBookingEntity RoomBooking { get; set; }
        public List<ServiceOrderEntity> ServiceOrders { get; set; }

     
    }
}
