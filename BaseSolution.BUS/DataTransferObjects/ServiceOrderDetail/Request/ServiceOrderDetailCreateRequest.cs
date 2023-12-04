using BaseSolution.Application.DataTransferObjects.Role.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request
{
    public class ServiceOrderDetailCreateRequest
    {
        public Guid ServiceId { get; set; }
        public Guid ServiceOrderId { get; set; }
        public decimal Price { get; set; }
        public double Amount { get; set; }
        public Guid? CreatedBy { get; set; }

        public class ServiceOrderDetailValication : AbstractValidator<ServiceOrderDetailCreateRequest>
        {
            public ServiceOrderDetailValication()
            {
                RuleFor(x => x.ServiceId).NotEmpty().WithMessage("ServiceId cannot be empty.").NotEqual(Guid.Empty).WithMessage("ServiceId cannot be empty Guid.");
                RuleFor(x => x.ServiceOrderId).NotEmpty().WithMessage("ServiceOrderId cannot be empty.").NotEqual(Guid.Empty).WithMessage("ServiceOrderId cannot be empty Guid.");
                RuleFor(x => x.Price).NotEmpty().WithMessage("ServiceOrderId cannot be empty.");
                RuleFor(x => x.Amount).NotEmpty().WithMessage("ServiceOrderId cannot be empty.");


            }
        }

    }
}
