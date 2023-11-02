using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Domain.Entities
{
    public class AmenityRoomDetailEntity :IEntityBase
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int Amount { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public AmenityEntity Amenity { get; set; }
        public RoomTypeEntity RoomType { get; set; }
    }
}
