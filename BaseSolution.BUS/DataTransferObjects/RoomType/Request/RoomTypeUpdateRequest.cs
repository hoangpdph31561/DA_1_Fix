using BaseSolution.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.RoomType.Request
{
    public class RoomTypeUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
        public class RoomTypeValication : AbstractValidator<RoomTypeUpdateRequest>
        {
            public RoomTypeValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");


            }
        }
    }
}
