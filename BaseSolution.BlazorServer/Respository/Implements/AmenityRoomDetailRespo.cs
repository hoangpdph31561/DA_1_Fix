using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class AmenityRoomDetailRespo : IAmenityRoomDetailRespo
    {
        private readonly HttpClient _httpClient;
        public AmenityRoomDetailRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateUpdateDeleteAmenityRoomDetail(List<AmenityRoomDetailCreateUpdateDelete> request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/AmenityRoomDetails/createUpdateDeleteAmenityRoomDetail", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<AmenityRoomDetailDTO>> GetAllAmentityRoomDetail(ViewAmenityRoomDetailWithPaginationRequest request)
        {
            string url = $"/api/AmenityRoomDetails?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if(request.RoomTypeId != Guid.Empty)
            {
                url = $"/api/AmenityRoomDetails?RoomTypeId={request.RoomTypeId}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<AmenityRoomDetailDTO>>(url);
            return result;
        }

        public async Task<PaginationResponse<AmenityRoomDetailDTO>> GetAmenityRoomDetailByAmenityId(ViewAmenityRoomDetailWithPaginationRequestAndAmenityId request)
        {
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<AmenityRoomDetailDTO>>($"/api/AmenityRoomDetails/getAmenityRoomDetailByAmenityId?AmenityId={request.AmenityId}&PageNumber={request.PageNumber}&PageSize={request.PageSize}");
            return result;
        }
    }
}
