using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.DataTransferObjects.Services;
using BaseSolution.Application.DataTransferObjects.Services.Request;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IServiceReadOnlyRepository
    {
        Task<RequestResult<ServiceDTO?>> GetServiceByIdAsync(Guid idService, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ServiceDTO>>> GetServiceWithPaginationByAdminAsync(
            ViewServiceWithPaginationRequest request, CancellationToken cancellationToken);

            Task<RequestResult<List<ServiceDTO>>> GetServiceAsync(
            ViewServiceWithPaginationRequest request, CancellationToken cancellationToken);


    }
}
