using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class AmenityCreateRequest
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
    public class AmenityValication : AbstractValidator<AmenityCreateRequest>
    {
        public AmenityValication()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
