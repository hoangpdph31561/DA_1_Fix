using FluentValidation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Floor.Request
{
    public class FloorCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public Guid? CreatedBy { get; set; }

        public class FloorValication : AbstractValidator<FloorCreateRequest>
        {
            public FloorValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");

                RuleFor(x => x.NumberOfRoom).NotNull().WithMessage("NumberOfRoom cannot be empty.");
                RuleFor(x => x.BuildingId).NotEmpty().WithMessage("BuildingId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid."); 

            }
        }
    }
}
