using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class RoomBookingDetailValication : AbstractValidator<RoomBookingDetailDeleteRequest>
        {
            public RoomBookingDetailValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
         
        }
    }
}
