using FluentValidation.Validators;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleCreateRequest
    {
        public string Name { get; set; } 
        public string RoleCode { get; set; }
        public Guid? CreatedBy { get; set; }
        public class RoleValication : AbstractValidator<RoleCreateRequest>
        {
            public RoleValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");

                RuleFor(x => x.RoleCode).NotEmpty().WithMessage("RoleCode cannot be empty.");
              
            }
        }
    }
}
