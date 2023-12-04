using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Account.request
{
    public class LoginInputRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid UserRoleId { get; set; }
        public class LoginValication : AbstractValidator<LoginInputRequest>
        {
            public LoginValication()
            {
                RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Pass word is required");
                RuleFor(x => x.UserRoleId).NotEmpty().WithMessage("User Role is required");
            }
        }
    }
}
