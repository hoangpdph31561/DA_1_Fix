using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomBookingRepo : IRoomBookingRepo
    {
        private readonly HttpClient _httpClient;
        public RoomBookingRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<RoomBookingDto>> GetListRoomBookingByOther(ViewRoombookingPaginationRequest request)
        {
            string url = $"/api/RoomBookings/getRoomBookingByDetail?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if (!String.IsNullOrEmpty(request.SearchString))
            {
                url = $"/api/RoomBookings/getRoomBookingByDetail?SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomBookingDto>>(url);
            return result;
        }
    }
}
