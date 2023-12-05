using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.RoomType.Request
{
    public class RoomTypeDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class RoomTypeDeleteValication : AbstractValidator<RoomTypeDeleteRequest>
        {
            public RoomTypeDeleteValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
