using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IBuildingReadWriteRespository
    {
        Task<RequestResult<Guid>> AddBuildingAsync(BuildingEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateBuildingAsync(BuildingEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteBuildingAsync(BuildingDeleteRequest request, CancellationToken cancellationToken);
    }
}
