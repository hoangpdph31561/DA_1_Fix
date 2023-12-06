using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public EntityStatus Status { get; set; }
        public class ServiceOrderUpdateValication : AbstractValidator<ServiceOrderUpdateRequest>
        {
            public ServiceOrderUpdateValication()
            {
                RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty.").NotEqual(Guid.Empty).WithMessage("CustomerId cannot be empty Guid.");
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
            }
        }
    }
}
