using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;
using Microsoft.Extensions.Primitives;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomTypeRespo : IRoomTypeRespo
    {
        private readonly HttpClient _httpClient;
        public RoomTypeRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewRoomType(RoomTypeCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/RoomTypes", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRoomType(RoomTypeDeleteRequest request)
        {
            string url = $"/api/RoomTypes?Id={request.Id}";
            if(!string.IsNullOrWhiteSpace(request.DeletedBy.ToString()) || request.DeletedBy != Guid.Empty)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<RoomTypeDTO>> GetAllRoomTypes(ViewRoomTypeWithPaginationRequest request)
        {
            string url = $"/api/RoomTypes?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if(!string.IsNullOrEmpty(request.SearchString))
            {
                url = $"/api/RoomTypes?SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomTypeDTO>>(url);
            return result;
        }

        public async Task<RoomTypeDTO> GetRoomTypeById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<RoomTypeDTO>($"/api/RoomTypes/{id}");
            return result;
        }

        public async Task<bool> UpdateRoomType(RoomTypeUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/RoomTypes", request);
            return result.IsSuccessStatusCode;
        }
    }
}
