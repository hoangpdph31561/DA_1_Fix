using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Services.Request
{
    public class ServiceDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class ServiceValication : AbstractValidator<ServiceDeleteRequest>
        {
            public ServiceValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
