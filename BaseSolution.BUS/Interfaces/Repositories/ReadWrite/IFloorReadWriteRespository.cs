using BaseSolution.Application.DataTransferObjects.Floor.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IFloorReadWriteRespository
    {
        Task<RequestResult<Guid>> AddFloorAsync(FloorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateFloorAsync(FloorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteFloorAsync(FloorDeleteRequest request, CancellationToken cancellationToken);
    }
}
