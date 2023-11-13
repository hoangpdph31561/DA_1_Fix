using BaseSolution.BlazorServer.Data.DataTransferObjects.Floor;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class FloorRespo : IFloorRespo
    {
        private readonly HttpClient _httpClient;
        public FloorRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateNewFloor(FloorCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Floors", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFloor(FloorDeleteRequest request)
        {
            string url = $"api/Floors?Id={request.Id}";
            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<FloorDTO>> GetAllFloors(ViewFloorWithPaginationRequest request)
        {
            string url = $"/api/Floors?BuildingId={request.BuildingId}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if(!String.IsNullOrWhiteSpace(request.SearchString))
            {
                url = $"/api/Floors?BuildingId={request.BuildingId}&SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<FloorDTO>>(url);
            return result;
        }

        public async Task<FloorDTO> GetFloorById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<FloorDTO>($"/api/Floors/{id}");
            return result;
        }

        public async Task<bool> UpdateFloor(FloorUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Floors", request);
            return result.IsSuccessStatusCode;
        }
    }
}
