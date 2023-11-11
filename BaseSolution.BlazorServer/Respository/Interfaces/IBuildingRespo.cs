using BaseSolution.BlazorServer.Data.DataTransferObjects.Building;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Building.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IBuildingRespo
    {
        Task<PaginationResponse<BuildingDTO>> GetAllBuilding(ViewBuildingWithPaginationRequest request);
        Task<bool> DeleteBuilding(BuildingDeleteRequest request);
        Task<bool> CreateBuilding(BuildingCreateRequest request);
        Task<bool> UpdateBuilding(BuildingUpdateRequest request);
        Task<BuildingDTO> GetBuildingById(Guid id);
    }
}
