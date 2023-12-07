using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill.Request
{
    public class BillCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public Guid? RoomBookingId { get; set; }
        public class BillValication : AbstractValidator<BillCreateRequest>
        {
            public BillValication()
            {
                RuleFor(x => x.CustomerId)
             .NotEmpty().WithMessage("CustomerId cannot be empty.")
             .NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
            }
        }
    }
}
