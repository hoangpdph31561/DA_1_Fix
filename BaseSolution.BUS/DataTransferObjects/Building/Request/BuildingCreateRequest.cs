using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Building.Request
{
    public class BuildingCreateRequest
    {
        public string Name { get; set; } = null!;
        public Guid? CreatedBy { get; set; }
        public class BuildingValication : AbstractValidator<BuildingCreateRequest>
        {
            public BuildingValication()
            {
                RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name cannot be empty.");
            }
        }
    }
}
