using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomDetailRespo : IRoomDetailRespo
    {
        private readonly HttpClient _httpClient;
        public RoomDetailRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<RoomDetailDTO>> GetAllRoomDetails(ViewRoomDetailWithPaginationRequest request)
        {
            string url = $"/api/RoomDetails?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomDetailDTO>>(url);
            return result;
        }
    }
}
