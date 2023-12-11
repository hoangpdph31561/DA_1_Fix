using BaseSolution.Domain.Enums;
using FluentValidation.Validators;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public Guid UserRoleId { get; set; }
        public EntityStatus Status { get; set; }

        public class UserValication : AbstractValidator<UserUpdateRequest>
        {
            public UserValication()
            {
                RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number cannot be empty.")

                                            .Matches(@"^\d+$").WithMessage("Please enter a valid phone number (digits only).");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Phone number cannot be empty.")

                                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Invalid email address.");
                RuleFor(x => x.UserRoleId).NotEmpty().WithMessage("UserRoleId cannot be empty.").NotEqual(Guid.Empty).WithMessage("UserRoleId cannot be empty Guid.");
            }
        }
    }
}
