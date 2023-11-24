using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.RoomType
{
    public class RoomTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfAmenityRoomDetail { get; set; }
        public int AmountOfRoomType { get; set; }
        public EntityStatus Status { get; set; }
        //Số lượng loại phòng này trong khách sạn
        public int NumberOfRoomDetails { get; set; }
        //Giá max của phòng
        public decimal MaxPriceOfRoom { get; set; }
        //Giá min của phòng
        public decimal MinPriceOfRoom { get; set; }
        //Số lượng tiện ích có trong loại phòng
        public int NumberOfAmenities { get; set; }
    }
}
