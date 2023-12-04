using BaseSolution.Application.DataTransferObjects.Role.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.RoomDetail.Request
{
    public class RoomDetailCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxPeopleStay { get; set; }
        public string Description { get; set; } = string.Empty;
        public double RoomSize { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Guid FloorId { get; set; }
        public Guid RoomTypeId { get; set; }
        public Guid? CreatedBy { get; set; }
        public class RoomDetailValication : AbstractValidator<RoomDetailCreateRequest>
        {
            public RoomDetailValication()
            {
                RuleFor(x => x.RoomTypeId).NotEmpty().WithMessage("RoomTypeId cannot be empty.").NotEqual(Guid.Empty).WithMessage("RoomTypeId cannot be empty Guid.");
                RuleFor(x => x.FloorId).NotEmpty().WithMessage("FloorId cannot be empty.").NotEqual(Guid.Empty).WithMessage("FloorId cannot be empty Guid.");
                RuleFor(x => x.RoomTypeId).NotEmpty().WithMessage("RoomTypeId cannot be empty.").NotEqual(Guid.Empty).WithMessage("RoomTypeId cannot be empty Guid.");
                RuleFor(x => x.Price).NotEmpty().WithMessage("Price cannot be empty.");
                RuleFor(x => x.MaxPeopleStay).NotEmpty().WithMessage("MaxPeopleStay cannot be empty.");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");
            }
        }
    }
}
