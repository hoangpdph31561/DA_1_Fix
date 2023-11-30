using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IRoomDetailReadWriteRepository
    {
        Task<RequestResult<Guid>> AddRoomDetailAsync(RoomDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRoomDetailAsync(RoomDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteRoomDetailAsync(RoomDetailDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateStatusRoomDetailAsync(RoomDetailUpdateStatusRequest request, CancellationToken cancellationToken);
    }
}
