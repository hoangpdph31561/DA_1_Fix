using BaseSolution.Domain.Entities.Base;
using BaseSolution.Domain.Enums;

namespace BaseSolution.Domain.Entities
{
    public class ExampleEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
    }
}
