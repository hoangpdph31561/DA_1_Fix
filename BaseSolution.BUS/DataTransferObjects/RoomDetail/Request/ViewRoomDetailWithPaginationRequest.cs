using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.RoomDetail.Request
{
    public class ViewRoomDetailWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
        public Guid? BuildingId { get; set; } = Guid.Empty;
        public Guid? FloorId { get; set; } = Guid.Empty;
        public Guid? RoomTypeId { get; set; } = Guid.Empty;
    }
}
