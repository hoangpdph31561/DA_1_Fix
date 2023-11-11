using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Building.Request
{
    public class ViewBuildingWithPaginationRequest : PaginationRequest
    {
        public string Search { get; set; } = string.Empty;
    }
}
