using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Domain.Entities
{
    public class FloorEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public BuildingEntity Building { get; set; }
        public List<RoomDetailEntity> RoomDetails { get; set; }

    }
}
