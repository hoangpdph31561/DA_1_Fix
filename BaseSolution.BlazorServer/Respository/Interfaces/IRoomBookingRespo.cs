using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomBookingRespo
    {
        Task<PaginationResponse<RoomBookingDto>> GetAllRoomBooking(ViewRoombookingPaginationRequest request);
        Task<bool> CreateNewRoomBooking(RoombookingCreateRequest request);
        Task<bool> UpdateRoomBooking(RoombookingUpdateRequest request);
        Task<RoomBookingDto> GetRoomBookingById(Guid id);

    }
}
