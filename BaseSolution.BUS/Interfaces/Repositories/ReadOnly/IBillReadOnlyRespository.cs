using BaseSolution.Application.DataTransferObjects.Bill;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IBillReadOnlyRespository
    {
        Task<RequestResult<BillDTO?>> GetBillByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByAdminAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByOtherAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
