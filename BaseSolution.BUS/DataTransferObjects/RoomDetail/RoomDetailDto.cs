using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.RoomDetail
{
    public class RoomDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxPeopleStay { get; set; }
        public string Description { get; set; } = string.Empty;
        public double RoomSize { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Guid FloorId { get; set; }
        public Guid RoomTypeId { get; set; }
        public RoomStatus Status { get; set; }
        //Tên tầng 
        public string FloorName { get; set; } = string.Empty;
        //Tên loại phòng
        public string RoomTypeName { get; set; } = string.Empty;
        //ID tòa nhà
        public Guid BuildingId { get; set; }
        //Tên tòa nhà
        public string BuildingName { get; set; } = string.Empty;
        //Status floor
        public EntityStatus FloorStatus { get; set; }
        //Status Building
        public EntityStatus BuildingStatus { get; set; }
        //Status roomType
        public EntityStatus RoomTypeStatus { get; set; }

        // để check xem trong khoảng tg này có phòng nào trống không !
        public DateTimeOffset CheckInBooking { get; set; }
        public DateTimeOffset CheckOutBooking { get; set; }


    }
}
