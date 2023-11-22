using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.ServiceOrder.Request
{
    public class ViewServiceOrderWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}

