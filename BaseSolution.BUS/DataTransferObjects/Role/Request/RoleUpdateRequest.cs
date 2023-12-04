using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleUpdateRequest
    {
        public Guid Id { get; set; }
        public string RoleCode { get; set; } = string.Empty;
        public class RoleValication : AbstractValidator<RoleUpdateRequest>
        {
            public RoleValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Name cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid."); 

                RuleFor(x => x.RoleCode).NotEmpty().WithMessage("RoleCode cannot be empty.");

            }
        }
    }
}
