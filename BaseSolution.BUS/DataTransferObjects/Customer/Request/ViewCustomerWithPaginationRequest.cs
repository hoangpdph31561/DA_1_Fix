using BaseSolution.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.DataTransferObjects.Customer.Request
{
    public class ViewCustomerWithPaginationRequest : PaginationRequest
    {
        public string? Name { get; set; } 
    }
}
