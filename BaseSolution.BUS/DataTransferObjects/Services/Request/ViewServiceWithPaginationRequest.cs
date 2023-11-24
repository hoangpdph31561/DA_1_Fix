using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Services.Request
{
    public class ViewServiceWithPaginationRequest : PaginationRequest
    {
        public Guid ServiceTypeId { get; set; }
        public string? SearchString { get; set; }
    }
}
