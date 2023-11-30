using BaseSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.RoomDetail.Request
{
    public class RoomDetailUpdateStatusRequest
    {
        public Guid Id { get; set; }
        public RoomStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
