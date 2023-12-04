using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill.Request
{
    public class BillUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? RoomBookingId { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public class BillValication : AbstractValidator<BillUpdateRequest>
        {
            public BillValication()
            {
                RuleFor(x => x.CustomerId)
             .NotEmpty().WithMessage("CustomerId cannot be empty.")
             .NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                
                RuleFor(x => x.Id)
             .NotEmpty().WithMessage("RoomTypeId cannot be empty.")
             .NotEqual(Guid.Empty).WithMessage("RoomTypeId cannot be empty Guid.");

            }
        }
    }
}
