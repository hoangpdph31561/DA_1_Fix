using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Bill.Request
{
    public class ViewBillWithPaginationRequest : PaginationRequest
    {
        public DateTimeOffset CreatedTime { get; set; }
        public string? SearchString { get; set; }

    }
}
