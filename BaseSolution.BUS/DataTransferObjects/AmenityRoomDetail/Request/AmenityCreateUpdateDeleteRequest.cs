using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityCreateUpdateDeleteRequest
    {
        public Guid RoomTypeId { get; set; }
        public Guid AmenityId { get; set; }
        public int Amount { get; set; }


        public class AmenityRoomValication : AbstractValidator<AmenityCreateUpdateDeleteRequest>
        {
            public AmenityRoomValication()
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

