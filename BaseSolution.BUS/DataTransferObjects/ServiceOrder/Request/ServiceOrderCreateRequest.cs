using BaseSolution.Application.DataTransferObjects.Role.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public decimal Price { get; set; }

        public class ServiceOrderValication : AbstractValidator<ServiceOrderCreateRequest>
        {
            public ServiceOrderValication()
            {
                RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                RuleFor(x => x.ServiceId).NotEmpty().WithMessage("ServiceId cannot be empty.").NotEqual(Guid.Empty).WithMessage("ServiceId cannot be empty Guid.");
            }
        }
    }
}
