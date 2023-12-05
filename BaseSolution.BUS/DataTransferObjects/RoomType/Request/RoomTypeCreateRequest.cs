using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Domain.Enums;
using FluentValidation;

namespace BaseSolution.Application.DataTransferObjects.RoomType.Request
{
    public class RoomTypeCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
        public class RoomTypeCreateValication : AbstractValidator<RoomTypeCreateRequest>
        {
            public RoomTypeCreateValication()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");


            }
        }
    }
}
