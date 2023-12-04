using BaseSolution.Application.DataTransferObjects.Role.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceType.Request
{
    public class ServiceTypeCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }

        public class ServiceTypeValication : AbstractValidator<ServiceTypeCreateRequest>
        {
            public ServiceTypeValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
            }
        }
    }
}
