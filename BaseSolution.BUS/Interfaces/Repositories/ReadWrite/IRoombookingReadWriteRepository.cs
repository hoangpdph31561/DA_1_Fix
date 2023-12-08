using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.DataTransferObjects.RoomBooking.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IRoombookingReadWriteRepository
    {
        Task<RequestResult<Guid>> AddRoomBookingAsync(RoomBookingEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<Guid>> AddRoomBookingByCustomerAsync(RoomBookingEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRoomBookingAsync(RoomBookingEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateStatusRoomBookingAsync(RoomBookingUpdateStatusRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteRoomBookingAsync(RoombookingDeleteRequest request, CancellationToken cancellationToken);
    }
}
