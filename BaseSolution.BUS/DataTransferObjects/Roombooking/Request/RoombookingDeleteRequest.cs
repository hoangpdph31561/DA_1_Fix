using BaseSolution.Application.DataTransferObjects.Role.Request;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class RoomBookingValication : AbstractValidator<RoombookingDeleteRequest>
        {
            public RoomBookingValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
