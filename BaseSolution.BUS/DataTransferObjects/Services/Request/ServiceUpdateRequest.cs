using BaseSolution.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Services.Request
{
    public class ServiceUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public bool IsRoomBookingNeed { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Guid ServiceTypeId { get; set; }
        public class ServiceValication : AbstractValidator<ServiceUpdateRequest>
        {
            public ServiceValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");
                RuleFor(x => x.Price).NotEmpty().WithMessage("Price cannot be empty.");
                RuleFor(x => x.Unit).NotEmpty().WithMessage("Unit cannot be empty.");
                RuleFor(x => x.ServiceTypeId).NotEmpty().WithMessage("ServiceTypeId cannot be empty.").NotEqual(Guid.Empty).WithMessage("ServiceTypeId cannot be empty Guid.");
            }
        }
    }
}
