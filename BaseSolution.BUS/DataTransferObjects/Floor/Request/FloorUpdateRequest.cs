using BaseSolution.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Floor.Request
{
    public class FloorUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int? NumberOfRoom { get; set; }
        public Guid? BuildingId { get; set; }
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
        public class FloorValication : AbstractValidator<FloorUpdateRequest>
        {
            public FloorValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");

                RuleFor(x => x.NumberOfRoom).NotNull().WithMessage("NumberOfRoom cannot be empty.");
                RuleFor(x => x.BuildingId).NotEmpty().WithMessage("BuildingId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                RuleFor(x => x.Status).IsInEnum().WithMessage("BuildingId cannot be empty.");

            }
        }
    }
}
