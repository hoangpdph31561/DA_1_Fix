using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class AmenityCreateUpdateDeleteRequest
    {
        public Guid RoomTypeId { get; set; }
        public Guid AmenityId { get; set; }
        public int Amount { get; set; }
    }
}
