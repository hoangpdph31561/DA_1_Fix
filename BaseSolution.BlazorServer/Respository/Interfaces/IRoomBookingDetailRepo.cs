using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomBookingDetailRepo 
    {
       Task<RoomBookingDetailDTO> GetRoomBookingDetailByRoomBookingId(Guid idRoomBooking);
       Task<PaginationResponse<RoomBookingDetailDTO>> GetRoomBookingDetailAsync(ViewRoomBookingDetailRequest roomBookingDetailRequest);
       Task<bool> UpdateRoomBookingDetail(RoomBookingDetailUpdateRequest request);
       Task<bool> UpdateRoomBookingDetail2(RoomBookingDetailUpdate2Request request);
        Task<bool> CreateNewRoomBookingDetail(RoomBookingDetailCreateRequest request);
        Task<bool> DeleteRoomBookingDetail(RoomBookingDetailDeleteRequest request);
    }
}
