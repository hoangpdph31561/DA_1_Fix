using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity
{
    public class AmenityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        //Tổng số trong khách sạn
        public int Total { get; set; }
        //Tổng số phòng sử dụng amenity
        public int NumberOfRoomUse { get; set; }
        
       
    }
}
