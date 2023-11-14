using BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class AmenityRespo : IAmenityRespo
    {
        private readonly HttpClient _httpClient;
        public AmenityRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewAmenity(CreateAmenityRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Amenities",request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAmenity(DeleteAmenityRequest request)
        {
            string url = $"/api/Amenities?Id={request.Id}";
            if(request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<AmenityDTO>> GetAllAmentity(ViewAmenityWithPaginationRequest request)
        {
            string url = $"/api/Amenities?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if(!String.IsNullOrWhiteSpace(request.SearchString))
            {
                url = $"/api/Amenities?SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<AmenityDTO>>(url);
            return result;
        }

        public async Task<AmenityDTO> GetAmenityById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<AmenityDTO>($"/api/Amenities/{id}");
            return result;
        }

        public async Task<bool> UpdateAmenity(UpdateAmenityRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Amenities", request);
            return result.IsSuccessStatusCode;
        }
    }
}
