using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Roombooking.Request
{
    public class RoombookingCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid Id { get; set; } = new Guid();
        public Guid RoomDetailId { get; set; }
        public BookingType BookingType { get; set; }
        public string CodeBooking { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public decimal Price { get; set; }


        public class RoomBookingValication : AbstractValidator<RoombookingCreateRequest>
        {
            public RoomBookingValication()
            {
                RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                RuleFor(x => x.RoomDetailId).NotEmpty().WithMessage("RoomDetailId cannot be empty.").NotEqual(Guid.Empty).WithMessage("RoomDetailId cannot be empty Guid.");
                RuleFor(x => x.BookingType).IsInEnum().WithMessage("BookingType cannot be empty.");
                RuleFor(x => x.CodeBooking).NotEmpty().WithMessage("CodeBooking cannot be empty.");

                RuleFor(x => x.CheckInBooking).NotEmpty().WithMessage("CheckInBooking cannot be empty.")
                                              .LessThan(x => x.CheckOutBooking).WithMessage("CheckIn must be less than checkOut");

                RuleFor(x => x.CheckOutBooking).NotEmpty().WithMessage("CheckOutBooking property cannot be empty.")
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