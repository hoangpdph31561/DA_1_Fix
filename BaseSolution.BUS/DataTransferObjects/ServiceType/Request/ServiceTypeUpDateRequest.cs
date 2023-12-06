using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceType.Request
{
    public class ServiceTypeUpDateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
        public class ServiceTypeValication : AbstractValidator<ServiceTypeUpDateRequest>
        {
            public ServiceTypeValication()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.").NotEqual(Guid.Empty).WithMessage("Id cannot be empty Guid.");
                RuleFor(x => x.Name).NotEmpty().WithMessage("Id cannot be empty.");
            }
        }

    }
}
