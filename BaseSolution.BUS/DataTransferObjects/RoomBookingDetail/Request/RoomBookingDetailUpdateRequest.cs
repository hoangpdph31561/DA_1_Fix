using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public class RoomBookingDetailValication : AbstractValidator<RoomBookingDetailUpdateRequest>
        {
            public RoomBookingDetailValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");

                RuleFor(x => x.CheckOutReality).NotEmpty().WithMessage("CheckOutBooking property cannot be empty.")
                                             .Must(BeGreaterThanCurrentTime).WithMessage("CheckOutBooking must be greater than the current time.");

            }
            private bool BeGreaterThanCurrentTime(DateTimeOffset DateTimeOffset)
            {
                // Check if the DateTime is greater than the current time
                return DateTimeOffset > DateTimeOffset.UtcNow;
            }
        }
    }
}
