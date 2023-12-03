using BaseSolution.Application.DataTransferObjects.Account.request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.validation
{
    // là class chưa các role mà chúng ta muốn validate
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
