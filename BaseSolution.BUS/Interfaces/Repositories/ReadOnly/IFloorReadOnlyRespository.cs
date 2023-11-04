using BaseSolution.Application.DataTransferObjects.Floor;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IFloorReadOnlyRespository
    {
        Task<RequestResult<FloorDTO?>> GetFloorByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<FloorDTO>>> GetFloorWithPaginationByAdminAsync(ViewFloorWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<FloorDTO>>> GetFloorWithPaginationByOtherAsync(ViewFloorWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
