using BaseSolution.Domain.Enums;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Customer.Request
{
    public class CustomerCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ApprovedCode { get; set; }
        public DateTimeOffset? ApprovedCodeExpiredTime { get; set; }
        public CustomerType CustomerType { get; set; }

        public class CustomerValication : AbstractValidator<CustomerCreateRequest>
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
