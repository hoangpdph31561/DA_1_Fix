using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomBookingRepo
    {
        Task<PaginationResponse<RoomBookingDto>> GetListRoomBookingByOther(ViewRoombookingPaginationRequest request);
    }
}
