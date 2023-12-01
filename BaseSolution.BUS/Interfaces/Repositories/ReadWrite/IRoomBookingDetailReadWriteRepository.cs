using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IRoomBookingDetailReadWriteRepository
    {
        Task<RequestResult<Guid>> AddRoomBookingDetailAsync(RoomBookingDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRoomBookingDetailAsync(RoomBookingDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteRoomBookingDetailAsync(RoomBookingDetailDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRoomBookingDetail2Async(RoomBookingDetailUpdate2Request request, CancellationToken cancellationToken);
    }
}
