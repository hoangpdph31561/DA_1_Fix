using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Role.Request
{
    public class RoleDeleteRequest
    {
        public Guid Id { get; set; }
        public class RoleValication : AbstractValidator<RoleDeleteRequest>
        {
            public RoleValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Name cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");


            }
        }
    }
}
