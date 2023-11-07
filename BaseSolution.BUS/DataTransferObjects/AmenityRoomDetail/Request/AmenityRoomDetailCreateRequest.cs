using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityRoomDetailCreateRequest
    {

        public Guid AmenityId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int Amount { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
