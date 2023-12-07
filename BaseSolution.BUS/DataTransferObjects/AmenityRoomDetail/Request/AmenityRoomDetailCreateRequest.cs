using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityRoomDetailCreateRequest
    {

        public Guid AmenityId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int Amount { get; set; }
        public Guid? CreatedBy { get; set; }


        public class AmenityRoomCreateValication : AbstractValidator<AmenityRoomDetailCreateRequest>
        {
            public AmenityRoomCreateValication()
            {
                RuleFor(x => x.RoomTypeId)
             .NotEmpty().WithMessage("RoomTypeId cannot be empty.")
             .NotEqual(Guid.Empty).WithMessage("RoomTypeId cannot be empty Guid.");

                RuleFor(x => x.AmenityId)
           .NotEmpty().WithMessage("AmenityId cannot be empty.")
           .NotEqual(Guid.Empty).WithMessage("AmenityId cannot be empty Guid.");

                RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount cannot be empty.");
            }
        }
    }
}
