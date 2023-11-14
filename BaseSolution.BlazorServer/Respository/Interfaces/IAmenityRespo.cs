using BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IAmenityRespo
    {
        Task<PaginationResponse<AmenityDTO>> GetAllAmentity(ViewAmenityWithPaginationRequest request);
        Task<bool> CreateNewAmenity(CreateAmenityRequest request);
        Task<bool> UpdateAmenity(UpdateAmenityRequest request);
        Task<bool> DeleteAmenity(DeleteAmenityRequest request);
        Task<AmenityDTO> GetAmenityById(Guid id);
    }
}
