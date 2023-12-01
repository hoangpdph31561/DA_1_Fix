using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail.Request;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomBookingDetailRepo 
    {
       Task<RoomBookingDetailDTO> GetRoomBookingDetailByRoomBookingId(Guid idRoomBooking);
       Task<bool> UpdateRoomBookingDetail(RoomBookingDetailUpdateRequest request);
    }
}
