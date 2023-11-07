using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.DataTransferObjects.ServiceType;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IServiceTypeReadOnlyRepository 
    {
        Task<RequestResult<ServiceTypeDto?>> GetServiceTypeByIdAsync(Guid idServiceType, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ServiceTypeDto>>> GetServiceTypeWithPaginationByAdminAsync(
            ViewServiceTypeWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
