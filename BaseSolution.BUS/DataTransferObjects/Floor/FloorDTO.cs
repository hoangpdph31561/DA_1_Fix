using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Floor
{
    public class FloorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public EntityStatus Status { get; set; }
        //Tên tòa nhà chứa tầng
        public string BuildingName { get; set; } = string.Empty;
        //Số phòng đã được tạo để thuê
        public int NumberOfRoomRent { get; set; }
    }
}
