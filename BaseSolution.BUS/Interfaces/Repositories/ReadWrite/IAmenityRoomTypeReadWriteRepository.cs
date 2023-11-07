using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IRoomTypeReadWriteRepository
    {
        Task<RequestResult<Guid>> AddRoomTypeAsync(RoomTypeEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRoomTypeAsync(RoomTypeEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteRoomTypeAsync(RoomTypeDeleteRequest request, CancellationToken cancellationToken);
    }
}
