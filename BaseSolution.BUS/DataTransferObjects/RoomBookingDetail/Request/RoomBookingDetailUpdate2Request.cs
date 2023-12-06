using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request
{
    public class RoomBookingDetailUpdate2Request
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }
        public DateTimeOffset CheckInReality { get; set; }
        public DateTimeOffset CheckOutReality { get; set; }
        public decimal PrePaid { get; set; } = 0;
        public Guid RoomDetailId { get; set; }
        public Guid RoomBookingId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? ModifiedBy { get; set; }

        public class RoomBookingDetailValication : AbstractValidator<RoomBookingDetailUpdate2Request>
        {
            public RoomBookingDetailValication()
            {
                RuleFor(x => x.RoomDetailId).NotEmpty().WithMessage("RoomDetailId cannot be empty.").NotEqual(Guid.Empty).WithMessage("RoomDetailId cannot be empty Guid.");
                RuleFor(x => x.RoomBookingId).NotEmpty().WithMessage("RoomBookingId cannot be empty.").NotEqual(Guid.Empty).WithMessage("RoomDetailId cannot be empty Guid.");
                RuleFor(x => x.Price).NotEmpty().WithMessage("RoomBookingId cannot be empty.");

                RuleFor(x => x.CheckInBooking).NotEmpty().WithMessage("CheckInBooking cannot be empty.")
                                              .LessThan(x => x.CheckOutBooking).WithMessage("CheckIn must be less than checkOut")
                                              .Must(BeGreaterThanCurrentTime).WithMessage("CheckInBooking must be greater than the current time.");

                RuleFor(x => x.CheckInReality).NotEmpty().WithMessage("CheckInReality property cannot be empty.")
                                            .LessThan(x => x.CheckOutBooking).WithMessage("CheckIn must be less than checkOut")
                                            .Must(BeGreaterThanCurrentTime).WithMessage("CheckInReality must be greater than the current time.");

                RuleFor(x => x.CheckOutReality).NotEmpty().WithMessage("CheckOutReality property cannot be empty.")
                                            .Must(BeGreaterThanCurrentTime).WithMessage("CheckOutReality must be greater than the current time.");


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
