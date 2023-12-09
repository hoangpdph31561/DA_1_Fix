using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request
{
    public class ViewAmenityRoomDetailWithPaginationRequestAndAmenityId : PaginationRequest
    {
        public Guid AmenityId { get; set; } = Guid.Empty;
    }
}
