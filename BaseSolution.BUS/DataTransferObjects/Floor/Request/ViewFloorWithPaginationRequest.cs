using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Floor.Request
{
    public class ViewFloorWithPaginationRequest : PaginationRequest
    {
        public Guid BuildingId { get; set; }
        public string? SearchString { get; set; }
    }
}
