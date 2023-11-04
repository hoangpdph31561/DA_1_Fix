using BaseSolution.Application.DataTransferObjects.Building;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IBuildingReadOnlyRespository
    {
        Task<RequestResult<BuildingDTO?>> GetBuildingByIdAsync(Guid idBuilding, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BuildingDTO>>> GetBuildingWithPaginationByAdminAsync (ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<BuildingDTO>>> GetBuildingWithPaginationByOtherAsync (ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
