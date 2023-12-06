using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class AmenityUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? ModifiedBy { get; set; }

        public class AmenityValication : AbstractValidator<AmenityUpdateRequest>
        {
            public AmenityValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
                RuleFor(x => x.Status).IsInEnum().WithMessage("Status is required");
            }
        }
    }
}
