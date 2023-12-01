using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBookingDetail.Request;
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

        public async Task <RoombookingDetailDTO> GetRoomBookingDetailByRoomBookingId(Guid idRoomBooking)
        {
            try
            {
                string url = $"/api/RoomBookingDetails/getRoomBookingDetailByRoomBookingId?idRoomBooking={idRoomBooking}";
                var result = await _httpClient.GetFromJsonAsync<RoombookingDetailDTO>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
         
        }
        public async Task<bool> UpdateRoomBookingDetail(RoomBookingDetailUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/RoomBookingDetails", request);
            return result.IsSuccessStatusCode;
        }
    }
}
