using BaseSolution.BlazorServer.Data.DataTransferObjects.Building;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Building.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class BuildingRespo : IBuildingRespo
    {
        private readonly HttpClient _httpClient;
        public BuildingRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateBuilding(BuildingCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Buildings", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBuilding(BuildingDeleteRequest request)
        {
            string url = $"api/Buildings?Id={request.Id}";
            if(request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<BuildingDTO>> GetAllBuilding(ViewBuildingWithPaginationRequest request)
        {
            string url = $"/api/Buildings/getBuildingsByOther?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if (!String.IsNullOrEmpty(request.Search))
            {
                url = $"/api/Buildings/getBuildingsByOther?Search={request.Search}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<BuildingDTO>>(url);
            return result;
        }

        public async Task<BuildingDTO> GetBuildingById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<BuildingDTO>($"api/Buildings/{id}");
            return result;
        }

        public async Task<bool> UpdateBuilding(BuildingUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("api/Buildings", request);
            return result.IsSuccessStatusCode;
        }
    }
}
