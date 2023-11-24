using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomTypeRepo : IRoomTypeRepo
    {
        private readonly HttpClient _httpClient;
        public RoomTypeRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<RoomTypeDTO>> GetRoomType(ViewRoomTypeWithPaginationRequest request)
        {
            string url = $"/api/RoomTypes?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomTypeDTO>>(url);
            return result;
        }

        public async Task<RoomTypeDTO> GetRoomTypeById(Guid idRoomType)
        {
            var result = await _httpClient.GetFromJsonAsync<RoomTypeDTO>($"/api/RoomTypes/{idRoomType}");
            return result;
        }
    }
}
