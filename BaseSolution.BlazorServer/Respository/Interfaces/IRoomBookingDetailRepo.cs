using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomBookingDetailRepo
    {
        Task<RoomBookingDetailDTO> GetRoomBookingDetailByIdRoomBooking(Guid idRoomBooking);
        Task<bool> UpdateRoomBookingDetail(RoomBookingDetailDTO request);
    }
}
