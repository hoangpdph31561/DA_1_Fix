using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Domain.Entities
{
    public class ServiceOrderEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? RoomBookingDetailId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public CustomerEntity Customer { get; set; }
        public RoomBookingDetailEntity RoomBookingDetail { get; set; }
        public List<ServiceOrderDetailEntity> ServiceOrderDetails { get; set; }
    }
}
