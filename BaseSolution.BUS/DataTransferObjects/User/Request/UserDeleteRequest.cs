using BaseSolution.Application.DataTransferObjects.Role.Request;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.User.Request
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }

        public class UserValication : AbstractValidator<UserDeleteRequest>
        {
            public UserValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
