using BaseSolution.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public EntityStatus Status { get; set; }
        public class ServiceOrderValication : AbstractValidator<ServiceOrderUpdateRequest>
        {
            public ServiceOrderValication()
            {
                RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
