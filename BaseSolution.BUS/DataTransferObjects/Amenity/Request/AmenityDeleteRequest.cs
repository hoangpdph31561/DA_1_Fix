using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Amenity.Request
{
    public class AmenityDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class AmenityValication : AbstractValidator<AmenityDeleteRequest>
        {
            public AmenityValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            }
        }
    }
}
