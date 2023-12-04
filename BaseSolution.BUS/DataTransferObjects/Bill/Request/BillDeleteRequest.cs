using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill.Request
{
    public class BillDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class BillValication : AbstractValidator<BillDeleteRequest>
        {
            public BillValication()
            {
                RuleFor(x => x.Id)
             .NotEmpty().WithMessage("RoomTypeId cannot be empty.")
             .NotEqual(Guid.Empty).WithMessage("RoomTypeId cannot be empty Guid.");

            }
        }
    }
}
