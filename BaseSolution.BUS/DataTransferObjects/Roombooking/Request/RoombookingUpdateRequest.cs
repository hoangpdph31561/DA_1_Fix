using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid RoomDetailId { get; set; }
        public BookingType BookingType { get; set; }
        public Guid CustomerId { get; set; }
        public RoomStatus Status { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }


        public class RoomBookingValication : AbstractValidator<RoombookingUpdateRequest>
        {
            public RoomBookingValication()
            {
                RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                RuleFor(x => x.RoomDetailId).NotEmpty().WithMessage("RoomDetailId cannot be empty.").NotEqual(Guid.Empty).WithMessage("RoomDetailId cannot be empty Guid.");
                RuleFor(x => x.BookingType).IsInEnum().WithMessage("BookingType cannot be empty.");
                RuleFor(x => x.CheckOutReality).NotEmpty().WithMessage("DateTime property cannot be empty.")
                                             .Must(BeGreaterThanCurrentTime).WithMessage("DateTime must be greater than the current time.");

            }
            private bool BeGreaterThanCurrentTime(DateTimeOffset DateTimeOffset)
            {
                // Check if the DateTime is greater than the current time
                return DateTimeOffset > DateTimeOffset.UtcNow;
            }
        }
    }
}
