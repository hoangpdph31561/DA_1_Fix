using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomBookingDetailRepo : IRoomBookingDetailRepo
    {
        private readonly HttpClient _httpClient;

        public RoomBookingDetailRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoomBookingDetailDTO> GetRoomBookingDetailByIdRoomBooking(Guid idRoomBooking)
        {
            var result = await _httpClient.GetFromJsonAsync<RoomBookingDetailDTO>($"/api/RoomBookingDetails/{idRoomBooking}/details");
            return result;
        }

        public async Task<bool> UpdateRoomBookingDetail(RoomBookingDetailDTO request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/RoomBookingDetails", request);
            return result.IsSuccessStatusCode;
        }
    }
}
