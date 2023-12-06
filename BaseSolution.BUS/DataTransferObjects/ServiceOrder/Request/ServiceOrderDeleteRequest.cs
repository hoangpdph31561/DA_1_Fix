using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
        public class ServiceOrderDeleteValication : AbstractValidator<ServiceOrderDeleteRequest>
        {
            public ServiceOrderDeleteValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
