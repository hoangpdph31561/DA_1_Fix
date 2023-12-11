using FluentValidation.Validators;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.Customer.Request
{
    public class CustomerDetailUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? ModifiedBy { get; set; }
        public class CustomerValication : AbstractValidator<CustomerDetailUpdateRequest>
        {
            public CustomerValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");

                RuleFor(x => x.IdentificationNumber).NotEmpty().WithMessage("IdentificationNumber cannot be empty.");

                RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number cannot be empty.")
                    .Matches(@"^\d+$").WithMessage("Please enter a valid phone number (digits only)."); ;
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.")
                                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Invalid email address.");
            }
        }
    }
}
