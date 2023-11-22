using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceType.Request
{
    public class ViewServiceTypeWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
