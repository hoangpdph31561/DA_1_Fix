using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.RoomType.Request
{
    public class RoomTypeCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
